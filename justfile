# UiPath coded activities — Plus edition
# Usage: just <recipe>
# Install just: https://github.com/casey/just

set shell := ["pwsh", "-NoLogo", "-NoProfile", "-Command"]

# Local Studio NuGet feed — update to match your environment
local_nuget_feed := "C:\\Users\\Public\\Documents\\myNugetPackages"

default:
    @just --list

# Build solution — also produces NuGet package (GeneratePackageOnBuild in Packaging project)
build:
    dotnet build CPMForge.Template.Plus.slnx -c Release

# Run unit tests
test: build
    dotnet test CPMForge.Template.Plus.slnx -c Release --no-build

# Promote packed NuGet → local Studio feed
publish: build
    Copy-Item build/nupkg/CPMForge.Template.Plus.*.nupkg "{{local_nuget_feed}}" -Force

# Bump version in Directory.Build.props (usage: just bump 0.0.3)
bump ver:
    $v = "{{ver}}"; $p = "Directory.Build.props"; $c = (Get-Content $p -Raw) -replace '<Version>[^<]+</Version>', "<Version>$v</Version>" -replace '<AssemblyVersion>[^<]+</AssemblyVersion>', "<AssemblyVersion>$v.0</AssemblyVersion>" -replace '<FileVersion>[^<]+</FileVersion>', "<FileVersion>$v.0</FileVersion>"; $c | Set-Content $p -NoNewline -Encoding UTF8; Write-Host "Bumped to $v"
