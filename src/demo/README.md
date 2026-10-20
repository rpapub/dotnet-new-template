# CPMForge.Template demo process

A UiPath Studio process, not a .NET project: open it in Studio or run it with `uip rpa run`.
It is never built by `dotnet build` and never published.

## `Main.xaml`

Calls `Get Effective TplSection Config` with no section, so the library falls back to the
defaults it ships in `content/Config/TplSection.default.toml`, then logs where the config came from.
Expected log line: `TplSection config from: shipped defaults`.

## Before running

1. `just publish` packs the library and puts it in the local feed PublicLocal.
2. `project.json` pins `CPMForge.Template.Plus` to an exact version. After `just bump`, raise that pin
   too, but only once the new version has been packed successfully.
