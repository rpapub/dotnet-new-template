#Requires -Version 5.1
<#
.SYNOPSIS
    Verifies the Library Coded Activity / DesignPropertiesViewModel convention
    and validates key fields in project.json.

.DESCRIPTION
    Reads project.json from the repo root and exits with a warning if the
    project is not a Library. Then runs two groups of checks:

    project.json config:
      - outputType is "Library"
      - libraryOptions.includeOriginalXaml is false
      - every path in libraryOptions.privateWorkflows exists on disk
      - every fileName in designOptions.fileInfoCollection exists on disk
      - UiPath.CodedWorkflows is listed in dependencies when ViewModels are found

    Coded activity convention (docs/library-coded-activity-convention.md):
      - Every CodeActivity/AsyncCodeActivity/NativeActivity has a matching
        {Name}ViewModel class.
      - [Category] value equals the activity's namespace and starts with the
        project name from project.json.
      - [Category], [DisplayName], [Description] are present on the activity.
      - The ViewModel inherits DesignPropertiesViewModel, has an
        IDesignServices constructor, and calls PersistValuesChangedDuringInit().
      - Every DesignPropertiesViewModel subclass has a paired activity class.

    Regex-based and best-effort. Exits 0 on pass, 1 on violations.

.PARAMETER Root
    Repo root to scan. Defaults to the parent of this script's folder.
#>
[CmdletBinding()]
param(
    [string] $Root = (Split-Path -Parent $PSScriptRoot)
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$Root = (Resolve-Path $Root).Path
$violations = New-Object System.Collections.Generic.List[object]

function Add-Violation {
    param([string]$File, [string]$Message)
    $rel = $File
    if ($File.StartsWith($Root)) {
        $rel = $File.Substring($Root.Length).TrimStart('\', '/')
    }
    $violations.Add([pscustomobject]@{ File = $rel; Message = $Message })
}

# -- 1. Read and validate project.json -----------------------------------------

$projectJsonPath = Join-Path $Root 'project.json'
if (-not (Test-Path $projectJsonPath)) {
    Write-Warning "project.json not found at $projectJsonPath — skipping project config checks."
    $projectJson = $null
    $projectName = ''
} else {
    $projectJson = Get-Content -LiteralPath $projectJsonPath -Raw | ConvertFrom-Json
    $projectName = $projectJson.name

    # outputType
    $outputType = $projectJson.designOptions.outputType
    if ($outputType -ne 'Library') {
        Write-Warning "project.json outputType is ""$outputType"", not ""Library"". This script targets Library projects only."
        exit 0
    }

    # includeOriginalXaml
    $includeXaml = $projectJson.designOptions.libraryOptions.includeOriginalXaml
    if ($includeXaml -ne $false) {
        Add-Violation 'project.json' "libraryOptions.includeOriginalXaml should be false (current: $includeXaml)"
    }

    # privateWorkflows — each listed path must exist
    $privateWorkflows = $projectJson.designOptions.libraryOptions.privateWorkflows
    if ($privateWorkflows) {
        foreach ($wf in $privateWorkflows) {
            $wfPath = Join-Path $Root ($wf -replace '[\\/]', [IO.Path]::DirectorySeparatorChar)
            if (-not (Test-Path $wfPath)) {
                Add-Violation 'project.json' "privateWorkflows: ""$wf"" not found on disk"
            }
        }
    }

    # fileInfoCollection — each fileName must exist
    $fileInfoCollection = $projectJson.designOptions.fileInfoCollection
    if ($fileInfoCollection) {
        foreach ($entry in $fileInfoCollection) {
            $entryPath = Join-Path $Root ($entry.fileName -replace '[\\/]', [IO.Path]::DirectorySeparatorChar)
            if (-not (Test-Path $entryPath)) {
                Add-Violation 'project.json' "fileInfoCollection: ""$($entry.fileName)"" not found on disk"
            }
        }
    }
}

# -- 2. Collect all .cs files (excluding obj/, bin/, packages/) ----------------

$csFiles = Get-ChildItem -Path $Root -Recurse -Filter '*.cs' -File |
    Where-Object { $_.FullName -notmatch '[\\/](obj|bin|packages)[\\/]' }

# -- 3. Index classes: name -> { File, Namespace, Bases, Attributes, Content } -

$classes = @{}

$nsRegex    = [regex]'namespace\s+([\w.]+)'
$classRegex = [regex]'((?:\[[^\]]+\][ \t\r\n]*)*)public\s+(?:sealed\s+)?(?:abstract\s+)?(?:partial\s+)?class\s+(\w+)\s*:\s*([^\{]+?)\s*\{'

foreach ($file in $csFiles) {
    $content = Get-Content -LiteralPath $file.FullName -Raw
    $ns = ''
    $nsMatch = $nsRegex.Match($content)
    if ($nsMatch.Success) { $ns = $nsMatch.Groups[1].Value }

    foreach ($match in $classRegex.Matches($content)) {
        $name = $match.Groups[2].Value
        $classes[$name] = [pscustomobject]@{
            Name       = $name
            File       = $file.FullName
            Namespace  = $ns
            Bases      = $match.Groups[3].Value
            Attributes = $match.Groups[1].Value
            Content    = $content
        }
    }
}

# -- 4. Check every activity class ---------------------------------------------

$activityBaseRegex = '\b(?:CodeActivity|AsyncCodeActivity|NativeActivity)(?:<[^>]+>)?\b'
$viewModelCount = 0

foreach ($cls in $classes.Values) {
    if ($cls.Bases -notmatch $activityBaseRegex) { continue }
    if ($cls.Name.EndsWith('ViewModel')) { continue }  # guard against false match

    # [Category("...")]
    $catMatch = [regex]::Match($cls.Attributes, 'Category\(\s*"([^"]+)"\s*\)')
    if (-not $catMatch.Success) {
        Add-Violation $cls.File "$($cls.Name): missing [Category(""..."")] attribute"
    }
    else {
        $catValue = $catMatch.Groups[1].Value
        if ($catValue -ne $cls.Namespace) {
            Add-Violation $cls.File "$($cls.Name): [Category(""$catValue"")] does not match namespace ""$($cls.Namespace)"""
        }
        if ($projectName -and -not $catValue.StartsWith($projectName)) {
            Add-Violation $cls.File "$($cls.Name): [Category(""$catValue"")] does not start with project name ""$projectName"""
        }
    }

    if ($cls.Attributes -notmatch 'DisplayName\s*\(') {
        Add-Violation $cls.File "$($cls.Name): missing [DisplayName(...)] attribute"
    }
    if ($cls.Attributes -notmatch 'Description\s*\(') {
        Add-Violation $cls.File "$($cls.Name): missing [Description(...)] attribute"
    }

    # Companion ViewModel
    $vmName = "$($cls.Name)ViewModel"
    if (-not $classes.ContainsKey($vmName)) {
        Add-Violation $cls.File "$($cls.Name): no companion ""$vmName"" class found in repo"
        continue
    }

    $viewModelCount++
    $vm = $classes[$vmName]
    if ($vm.Bases -notmatch 'DesignPropertiesViewModel') {
        Add-Violation $vm.File "${vmName}: must inherit from DesignPropertiesViewModel"
    }
    if ($vm.Namespace -ne $cls.Namespace) {
        Add-Violation $vm.File "${vmName}: namespace ""$($vm.Namespace)"" does not match activity namespace ""$($cls.Namespace)"""
    }
    if ($vm.Content -notmatch "public\s+$vmName\s*\(\s*IDesignServices") {
        Add-Violation $vm.File "${vmName}: missing constructor ""public $vmName(IDesignServices services) : base(services)"""
    }
    if ($vm.Content -notmatch 'PersistValuesChangedDuringInit\s*\(') {
        Add-Violation $vm.File "${vmName}: InitializeModel() should call PersistValuesChangedDuringInit() before mutating properties"
    }
}

# -- 5. Check every DesignPropertiesViewModel subclass has a paired activity ---

foreach ($cls in $classes.Values) {
    if ($cls.Bases -notmatch 'DesignPropertiesViewModel') { continue }

    if (-not $cls.Name.EndsWith('ViewModel')) {
        Add-Violation $cls.File "$($cls.Name): DesignPropertiesViewModel subclass must be named ""<Activity>ViewModel"""
        continue
    }
    $activityName = $cls.Name.Substring(0, $cls.Name.Length - 'ViewModel'.Length)
    if (-not $classes.ContainsKey($activityName)) {
        Add-Violation $cls.File "$($cls.Name): no paired activity class ""$activityName"" found in repo"
    }
}

# -- 6. UiPath.CodedWorkflows dependency required when ViewModels exist --------

if ($projectJson -and $viewModelCount -gt 0) {
    $deps = $projectJson.dependencies.PSObject.Properties.Name
    if ($deps -notcontains 'UiPath.CodedWorkflows') {
        Add-Violation 'project.json' "UiPath.CodedWorkflows missing from dependencies ($viewModelCount ViewModel(s) found)"
    }
}

# -- 7. Report -----------------------------------------------------------------

if ($violations.Count -eq 0) {
    Write-Host "OK - $($classes.Count) classes scanned, library convention satisfied."
    exit 0
}

Write-Host "Violations ($($violations.Count)):"
foreach ($v in $violations) {
    Write-Host "  $($v.File): $($v.Message)"
}
exit 1
