# dotnet-new-template — UiPath Coded Activities (Plus Edition)

`dotnet new` template for UiPath VS-compiled coded activities: a typed config section read through CPMForge.Config.Plus, DesignPropertiesViewModel, split NuGet package, Workflow Analyzer rules, xunit tests and a Studio demo process.

---

## Quick Start

### 1. Install the template

```bash
# From NuGet (once published)
dotnet new install CPMForge.Template.Plus.Templates

# Or from a local clone
git clone https://github.com/rpapub/dotnet-new-template
dotnet new install ./dotnet-new-template
```

### 2. Scaffold your project

```bash
dotnet new cpmf-custom-activities -n Acme.MyActivities
```

### 3. Build and pack

```bash
cd Acme.MyActivities
just build      # or: dotnet build Acme.MyActivities.Plus.slnx -c Release
# → Acme.MyActivities.Plus.<version>.nupkg and Acme.MyActivities.WorkflowAnalyzerRules.<version>.nupkg in build/nupkg/
just publish    # → local feed PublicLocal (C:\Users\Public\NuGetFeed) via C:\Users\Public\Publish-ToNuGetFeed.ps1
```

---

## Template parameters

| Parameter | Default | Description |
|---|---|---|
| `-n` / `--name` | _(required)_ | Root namespace, e.g. `Acme.MyActivities` |
| `--authors` | `Christian Prior-Mamulyan` | Author in NuGet metadata and copyright |
| `--workflowCompilerVersion` | `26.0.0-alpha.22366` | `uipath.workflowcompiler.windows` version in `NUGET_PACKAGES` |
| `--runtimeDesignVersion` | `25.10.33` | `uipath.uiautomation.activities.runtime.design` version in `NUGET_PACKAGES` |

Example with all parameters:

```bash
dotnet new cpmf-custom-activities -n Acme.MyActivities \
  --authors "Jane Smith" \
  --workflowCompilerVersion "26.0.0-alpha.22366" \
  --runtimeDesignVersion "25.10.33"
```

---

## What you get

```
Acme.MyActivities/
├── Directory.Build.props                  ← SDK paths, CpmfConfigPlusVersion, package metadata (edit versions here)
├── nuget.config                           ← PublicLocal + T:\nuget-feed (CPMForge.RulesSdk) + nuget.org
├── justfile                               ← build/test/publish/bump shortcuts
├── manifest.json                          ← schema v3 repo manifest (edit name/description/category)
├── .github/workflows/release.yml          ← calls the shared pack workflow
├── Acme.MyActivities.Plus.slnx
└── src/
    ├── plus/
    │   ├── Config/MyActivities.default.toml    ← shipped defaults, packed into content/Config/
    │   ├── Acme.MyActivities/                  ← runtime assembly (net8.0-windows + net8.0)
    │   │   ├── Activities/GetEffectiveMyActivitiesConfig.cs
    │   │   ├── Configuration/                  ← [ConfigSection] class + its resolve logic
    │   │   └── Shared/                         ← async NativeActivity base classes
    │   ├── Acme.MyActivities.Api/              ← ICodedWorkflowsServiceRegistry
    │   ├── Acme.MyActivities.Design/           ← DesignPropertiesViewModel per activity (net8.0-windows)
    │   ├── Acme.MyActivities.Packaging/        ← produces Acme.MyActivities.Plus.nupkg (net8.0-windows + net8.0)
    │   ├── RPAForge.MyActivities.Packaging/    ← RPAForge twin: same binaries, depends on RPAForge.Config.Plus
    │   └── Acme.MyActivities.Tests/            ← xunit tests
    ├── rules/
    │   ├── Acme.MyActivities.Rules/            ← Workflow Analyzer rules on CPMForge.RulesSdk
    │   └── Acme.MyActivities.Rules.Packaging/  ← produces Acme.MyActivities.WorkflowAnalyzerRules.nupkg
    └── demo/                                   ← UiPath Studio process using the packed library
```

The config section, its class and its default file are named after the last segment of `-n`
(`Acme.MyActivities` → `MyActivities`). Replace the example key `Enabled` with the library's own settings.

---

## Prerequisites

- .NET SDK 8.0+
- Local feeds: `C:\Users\Public\NuGetFeed` (PublicLocal, holds CPMForge.Config.Plus) and `T:\nuget-feed` (holds CPMForge.RulesSdk)
- `NUGET_PACKAGES` environment variable pointing to your NuGet global packages folder
- `uipath.workflowcompiler.windows` extracted in `NUGET_PACKAGES`
- `uipath.uiautomation.activities.runtime.design` extracted in `NUGET_PACKAGES`

```powershell
# Verify
[System.Environment]::GetEnvironmentVariable("NUGET_PACKAGES")
# Expected: T:\nuget\packages  (or your equivalent)
```

Optionally `just` task runner for convenience recipes:

```bash
winget install Casey.Just
```

---

## Updating SDK versions

Edit `Directory.Build.props`:

```xml
<UiPathWorkflowCompilerVersion>26.0.0-alpha.22366</UiPathWorkflowCompilerVersion>
<UiPathRuntimeDesignVersion>25.10.33</UiPathRuntimeDesignVersion>
```

---

## License

Apache 2.0 — see [LICENSE](LICENSE).
