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
    dotnet build Cpmf.CustomActivities.Plus.slnx -c Release

# Run unit tests
test: build
    dotnet test Cpmf.CustomActivities.Plus.slnx -c Release --no-build

# Promote packed NuGet → local Studio feed
publish: build
    Copy-Item build/nupkg/Cpmf.CustomActivities.Plus.*.nupkg "{{local_nuget_feed}}" -Force
