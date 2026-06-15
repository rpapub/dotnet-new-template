# dotnet-new-template — UiPath Coded Activities (Plus Edition)

`dotnet new` template for UiPath VS-compiled coded activities: NativeActivity scope, ActivityDesigner chrome, DesignPropertiesViewModel, split NuGet package, xunit tests.

---

## Quick Start

### 1. Install the template

```bash
# From NuGet (once published)
dotnet new install Cpmf.CustomActivities.Plus.Templates

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
dotnet build -c Release
dotnet build Acme.MyActivities.Packaging/Acme.MyActivities.Packaging.csproj -c Release
# → Acme.MyActivities.Plus.0.0.1.nupkg in build/nupkg/
```

---

## Template parameters

| Parameter | Default | Description |
|---|---|---|
| `-n` / `--name` | _(required)_ | Root namespace, e.g. `Acme.MyActivities` |
| `--authors` | `Your Name` | Author in NuGet metadata and copyright |
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
├── Directory.Build.props              ← SDK paths + package metadata (edit versions here)
├── nuget.config
├── justfile                           ← build/test/pack/publish shortcuts
├── Acme.MyActivities.Plus.slnx
├── Acme.MyActivities/                 ← runtime assembly (net6.0 + net6.0-windows)
│   ├── Activities/
│   │   ├── WithVaultSession.cs        ← NativeActivity scope with typed delegate Body
│   │   ├── DocumentExists.cs
│   │   ├── ReadDocument.cs
│   │   └── WriteDocument.cs
│   ├── Models/                        ← AuthMode, VaultSession, VaultDocument, ...
│   ├── Shared/                        ← ContinuableAsyncNativeActivity base class
│   └── Exceptions/
├── Acme.MyActivities.Api/             ← ICodedWorkflowsServiceRegistry + VaultOperations
├── Acme.MyActivities.Design/          ← WPF designer chrome (net6.0-windows only)
│   ├── ViewModels/                    ← DesignPropertiesViewModel per activity
│   └── WithVaultSessionDesigner.xaml  ← ActivityDesigner with WorkflowItemPresenter
├── Acme.MyActivities.Packaging/       ← produces Acme.MyActivities.Plus.nupkg
└── Acme.MyActivities.Tests/           ← xunit tests
```

All activities throw `NotImplementedException` — they are opinionated stubs reflecting the target API. Implement them against your backend.

---

## Prerequisites

- .NET SDK 6.0+
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
