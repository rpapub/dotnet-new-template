# dotnet-new-template — UiPath Coded Activities (Plus Edition)

`dotnet new` template for UiPath VS-compiled coded activities: NativeActivity scope, split NuGet packages, ActivityDesigner chrome, xunit tests.

## Prerequisites

- .NET SDK 6.0+
- `NUGET_PACKAGES` environment variable pointing to your NuGet global packages folder
- `uipath.workflowcompiler.windows` extracted in `NUGET_PACKAGES`
- `uipath.uiautomation.activities.runtime.design` extracted in `NUGET_PACKAGES`
- `nuget` CLI on PATH
- `just` task runner (`winget install Casey.Just`)

```powershell
# Verify
[System.Environment]::GetEnvironmentVariable("NUGET_PACKAGES")
# Expected: T:\nuget\packages  (or your equivalent)
```

## Install template

```bash
# From NuGet (once published)
dotnet new install RPAForge.Sftp.Plus.Templates

# Or directly from this repo
dotnet new install https://github.com/rpapub/dotnet-new-template
```

## Use

```bash
dotnet new uipath-sftp-plus -n MyCompany.Ftp

# With explicit SDK versions (if yours differ from defaults)
dotnet new uipath-sftp-plus -n MyCompany.Ftp \
  --authors "Jane Smith" \
  --workflowCompilerVersion "26.0.0-alpha.22366" \
  --runtimeDesignVersion "25.10.33"
```

Produces:

```
MyCompany.Ftp/
├── Directory.Build.props          ← SDK paths + package metadata
├── nuget.config
├── justfile
├── MyCompany.Ftp.Plus.nuspec      ← meta-package
├── MyCompany.Ftp.Plus.Runtime.nuspec
├── MyCompany.Ftp.Plus.Runtime.Portable.nuspec
├── MyCompany.Ftp.Plus.Design.nuspec
├── MyCompany.Ftp.Plus.slnx
├── MyCompany.Ftp/                 ← runtime assembly (net6.0-windows + net6.0)
│   ├── Activities/
│   ├── Exceptions/
│   └── ...
├── MyCompany.Ftp.Design/          ← WPF designer chrome (net6.0-windows)
└── MyCompany.Ftp.Tests/           ← xunit tests
```

## Build scaffolded project

```bash
cd MyCompany.Ftp
just build    # dotnet build
just test     # dotnet test
just pack     # → build/nupkg/ (4 packages)
just publish  # copy to local Studio feed
```

## What the template provides

| | |
|---|---|
| `BuildConnection` | `NativeActivity` scope — injects `SftpConnection` via `context.Properties` |
| `ProbeConnection` | `CodeActivity` — tests reachability, sets `IsReachable` |
| `UploadFile` | `CodeActivity` stub |
| `DownloadFile` | `CodeActivity` stub |
| `SftpErrorClassifier` | Single `IsRetryable(Exception)` — layered error model |
| 6 typed exceptions | `SftpAuthException`, `SftpTimeoutException`, etc. |
| ActivityDesigner | `BuildConnectionDesigner.xaml` with `WorkflowItemPresenter` |
| Split packages | `.Runtime` / `.Runtime.Portable` / `.Design` / meta |

All activities are loud stubs (`NotImplementedException`) reflecting target state. Implement against the stub interface.

## Updating SDK versions

Edit `Directory.Build.props`:

```xml
<UiPathWorkflowCompilerVersion>26.0.0-alpha.22366</UiPathWorkflowCompilerVersion>
<UiPathRuntimeDesignVersion>25.10.33</UiPathRuntimeDesignVersion>
```

## License

Apache 2.0 — see [LICENSE](LICENSE).
