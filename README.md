# gh-badges-sharp

A C#/.NET port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges) library that powers [shields.io](https://shields.io)

## Overview

[![Build Status](https://dev.azure.com/ap0llo/OSS/_apis/build/status/gh-badges-sharp?branchName=master)](https://dev.azure.com/ap0llo/OSS/_build/latest?definitionId=15&branchName=master)
[![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)
[![Renovate](https://img.shields.io/badge/Renovate-enabled-brightgreen)](https://renovatebot.com/)

[![NuGet](https://img.shields.io/nuget/v/Grynwald.GhBadgesSharp.svg)](https://www.nuget.org/packages/Grynwald.GhBadgesSharp)
[![MyGet](https://img.shields.io/myget/ap0llo-gh-badges-sharp/vpre/Grynwald.GhBadgesSharp.svg?label=myget)](https://www.myget.org/feed/ap0llo-gh-badges-sharp/package/nuget/Grynwald.GhBadgesSharp)

GhBadgesSharp is a C#/.NET port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges) library that powers [shields.io](https://shields.io).

See [Acknowledgments](#acknowledgments) for license info.

## Installation

GhBadgesSharp is distributed as NuGet package.

- Prerelease and stable builds are available on [MyGet](https://www.myget.org/feed/Packages/ap0llo-gh-badges-sharp)
- Stable builds are also available on [NuGet.org](https://www.nuget.org/packages/Grynwald.GhBadgesSharp)

## Usage

After installation, a badge can be rendered by calling `Badge.MakeBadge`.
It will return the badge as an SVG image (as `XElement`).

The following code shows how to render this badge:

![Example Badge](./docs/img/examplebadge.svg)

```cs
using System.IO;
using System.Xml.Linq;
using Grynwald.GhBadgesSharp;

public class Program
{
    public static void Main(string[] args)
    {
        XElement badge = Badge.MakeBadge(
            style: BadgeStyle.Flat,
            leftText: "Hello",
            rightText: "World",
            color: NamedColor.Green
        );

        File.WriteAllText("badge.svg", badge.ToString());
    }
}

```

## Building from source

**Note:** This repository uses git submodules, clone using `git clone --recursive`

Building GhBadgesSharp from source requires the .NET 6 SDK (version 6.0.101 as specified in [global.json](./global.json)) and uses [Cake](https://cakebuild.net/) for the build.

To execute the default task, run

```ps1
.\build.ps1
```

This will build the project, run all tests and pack the NuGet package.
## Acknowledgments

This library is a C# port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges)
library.

- [gh-badges](https://github.com/badges/shields/tree/master/gh-badges) is
  licensed under [Creative Commons Zero v1.0 Universal license](https://github.com/badges/shields/blob/master/LICENSE).
- Portions of this library were ported from the [is-css-color](https://github.com/princejwesley/is-css-color)
  library, licensed under the [MIT License](https://github.com/princejwesley/is-css-color/blob/master/LICENSE)
- Portions of this library were ported from the [anafanafo](https://github.com/metabolize/anafanafo)
  library, licensed under the [MIT License](https://github.com/metabolize/anafanafo/blob/master/LICENSE)

Libraries used (aside from .NET Core and .NET Standard):

- [Json.NET](https://www.newtonsoft.com/json)
- [Fluid](https://github.com/sebastienros/fluid)
- [Nerdbank.GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning/)
- [SourceLink](https://github.com/dotnet/sourcelink)
- [ReportGenerator](https://github.com/danielpalme/ReportGenerator)
- [Cake](https://cakebuild.net/)
- [Cake.BuildSystems.Module](https://github.com/cake-contrib/Cake.BuildSystems.Module)
- [xUnit](http://xunit.github.io/)
- [ApprovalTests](https://github.com/approvals/ApprovalTests.Net)
- [PublicApiGenerator](https://github.com/JakeGinnivan/ApiApprover)

## Versioning and Branching

The version of this project is automatically derived from git and the information
in `version.json` using [Nerdbank.GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning):

- The master branch  always contains the latest version. Packages produced from
  master are always marked as pre-release versions (using the `-pre` suffix).
- Stable versions are built from release branches. Build from release branches
  will have no `-pre` suffix
- Builds from any other branch will have both the `-pre` prerelease tag and the git
  commit hash included in the version string

To create a new release branch use the [`nbgv` tool](https://www.nuget.org/packages/nbgv/):

```ps1
dotnet tool restore
dotnet tool run nbgv -- prepare-release
```
