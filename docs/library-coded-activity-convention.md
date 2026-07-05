# Library Coded Activity Convention

Applies to UiPath **Library** projects (`outputType: "Library"` in
`project.json`) that expose `CodeActivity`-based activities to Studio
consumers. Coded activities in process projects don't need this treatment —
only library-published ones render in the consumer's activity tree.

Without a `DesignPropertiesViewModel`, a coded activity appears as an empty
card in the Studio designer. This document defines the convention so the
pattern is reproducible across cpmf libraries.

## Why a ViewModel is required

XAML activities carry designer metadata (display names, tooltips, required
flags, ordering) in a `.xaml.json` sidecar. Coded activities have no
`.cs.json` equivalent. Instead, `uipcli` compiles any class inheriting
`System.Activities.DesignViewModels.DesignPropertiesViewModel` into
`{ProjectName}.ViewModels.dll`, which Studio reads to draw the activity card.

Without a ViewModel: the activity appears in the tree but its card is blank.

## The two files

### 1. Activity (`{Name}.cs`)

- Namespace **equals** the `[Category(...)]` value — each dot becomes a
  folder level in Studio's activity tree.
- Class-level attributes:
  - `[Category("ProjectName.SubArea.Group")]`
  - `[DisplayName("Human Readable Name")]`
  - `[Description("One-line summary.")]`
- Inherits `CodeActivity`, `AsyncCodeActivity`, or `NativeActivity`.
- Public properties are `InArgument<T>` / `OutArgument<T>`; required
  inputs carry `[RequiredArgument]`.

### 2. ViewModel (`{Name}ViewModel.cs`)

- Class name is `{ActivityName}ViewModel`.
- Same namespace as the activity.
- Inherits `DesignPropertiesViewModel`.
- Constructor takes `IDesignServices` and passes it to `base`.
- Mirrors activity arguments as `DesignInArgument<T>` / `DesignOutArgument<T>`
  with **matching property names**.
- Overrides `InitializeModel()`:
  1. Call `base.InitializeModel()`.
  2. Call `PersistValuesChangedDuringInit()` **before** mutating any property.
  3. Set `DisplayName`, `Tooltip`, `IsRequired`, `IsPrincipal`, `OrderIndex`
     on each mirrored property.

Canonical example in this repo: `GetEffectiveConfig.cs` +
`GetEffectiveConfigViewModel.cs`.

## Verification

From the repo root:

```
pwsh ./scripts/Check-LibraryCodedActivityConvention.ps1
```

Exits `0` on pass, `1` on violations. Exits `0` with a warning if
`project.json` is not a Library project. Suitable for CI or a pre-commit hook.

**project.json checks:**

- `designOptions.outputType == "Library"` — script is a no-op for other project types
- `libraryOptions.includeOriginalXaml == false`
- Every path in `libraryOptions.privateWorkflows` exists on disk
- Every `fileName` in `designOptions.fileInfoCollection` exists on disk
- `UiPath.CodedWorkflows` is in `dependencies` when any ViewModel is found

**Coded activity checks:**

- Every `CodeActivity` / `AsyncCodeActivity` / `NativeActivity` has a matching
  `{Name}ViewModel` class.
- `[Category]` value matches the activity's namespace and starts with the
  project `name` from `project.json`.
- `[Category]`, `[DisplayName]`, `[Description]` are present on the activity.
- The ViewModel inherits `DesignPropertiesViewModel`, has an
  `IDesignServices` constructor, and calls `PersistValuesChangedDuringInit()`.
- Every `DesignPropertiesViewModel` subclass has a paired activity class.

The checks are regex-based and best-effort — they catch the common
mistakes, not every corner case. Treat violations as signals, not proof.

## Reusing across libraries

Copy `docs/library-coded-activity-convention.md` and
`scripts/Check-LibraryCodedActivityConvention.ps1` into any cpmf library
repo. The script takes no configuration — it scans `*.cs` below the repo
root (excluding `obj/`, `bin/`, `packages/`).
