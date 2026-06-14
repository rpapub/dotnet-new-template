# UiPath coded activities — Plus edition
# Usage: just <recipe>
# Install just: https://github.com/casey/just

set shell := ["pwsh", "-NoLogo", "-NoProfile", "-Command"]

# NuGet package metadata — mirrors Directory.Build.props
pkg_version   := "0.0.1"
pkg_authors   := "Christian Prior-Mamulyan"
pkg_copyright := "Copyright (c) 2026 Christian Prior-Mamulyan"
_nuget_props  := "version=" + pkg_version + ";authors=" + pkg_authors + ";copyright=" + pkg_copyright

# Local Studio NuGet feed — update to match your environment
local_nuget_feed := "C:\\Users\\Public\\Documents\\myNugetPackages"

default:
    @just --list

# Build coded activities (requires dotnet SDK)
build:
    dotnet build Cpmf.CodedActivities.Plus.slnx -c Release

# Run unit tests
test: build
    dotnet test Cpmf.CodedActivities.Plus.slnx -c Release --no-build

# Pack NuGets (meta + runtime + portable + design) → build/nupkg/
pack: build
    nuget pack Cpmf.CodedActivities.Plus.nuspec -Properties "{{_nuget_props}}" -OutputDirectory "build/nupkg"
    nuget pack Cpmf.CodedActivities.Plus.Runtime.nuspec -Properties "{{_nuget_props}}" -OutputDirectory "build/nupkg"
    nuget pack Cpmf.CodedActivities.Plus.Runtime.Portable.nuspec -Properties "{{_nuget_props}}" -OutputDirectory "build/nupkg"
    nuget pack Cpmf.CodedActivities.Plus.Design.nuspec -Properties "{{_nuget_props}}" -OutputDirectory "build/nupkg"

# Promote packed NuGets → local Studio feed
publish: pack
    Copy-Item build/nupkg/Cpmf.CodedActivities.Plus.*.nupkg "{{local_nuget_feed}}" -Force
