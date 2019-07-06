# gh-badges-sharp

A C#/.NET port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges)
library that powers [shields.io](https://shields.io)

**TODO**:

- *Adjust settings for CI builds in `azure-pipelines.yml`*
- *Adjust the following sections in this README file*
- *Remove this section from this README file*

## Overview

*__TODO:__ Add status badges for package(s) on NuGet.org (and MyGet), build status badge for Azure Pipeline*

## Installation

*__TODO:__ PACKAGENAME* is distributed as NuGet package.

- Prerelease builds are available on [MyGet](https://example.com) **TODO:** Provide package urls
- Release versions are available on [NuGet.org](https://example.com) **TODO:** Provide package urls

## Building from source

```bat
  dotnet restore .\src\GhBadgesSharp.sln

  dotnet build .\src\GhBadgesSharp.sln

  dotnet pack .\src\GhBadgesSharp.sln
```

## Acknowledgments

This library is a C# port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges)
library and some of it's dependencies.

- [gh-badges](https://github.com/badges/shields/tree/master/gh-badges) is
  licensed under [Creative Commons Zero v1.0 Universal license](https://github.com/badges/shields/blob/master/LICENSE).
- Portions of this library were ported from the [is-css-color](https://github.com/princejwesley/is-css-color)
  library, licensed under the [MIT License](https://github.com/princejwesley/is-css-color/blob/master/LICENSE)

__TODO:__ Add remaining acknoledgements

## Versioning and Branching

The version of this library is automatically derived from git and the information
in `version.json` using [Nerdbank.GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning):

- The master branch  always contains the latest version. Packages produced from
  master are always marked as pre-release versions (using the `-pre` suffix).
- Stable versions are built from release branches. Build from release branches
  will have no `-pre` suffix
- Builds from any other branch will have both the `-pre` prerelease tag and the git
  commit hash included in the version string

To create a new release branch use the [`nbgv` tool](https://www.nuget.org/packages/nbgv/)
(at least version `3.0.4-beta`):

```ps1
dotnet tool install --global nbgv --version 3.0.4-beta
nbgv prepare-release
```
