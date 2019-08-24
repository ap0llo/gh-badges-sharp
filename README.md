# gh-badges-sharp

A C#/.NET port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges)
library that powers [shields.io](https://shields.io)

## Overview

[![NuGet](https://img.shields.io/nuget/v/Grynwald.GhBadgesSharp.svg)](https://www.nuget.org/packages/Grynwald.GhBadgesSharp)

[![Build Status](https://dev.azure.com/ap0llo/OSS/_apis/build/status/gh-badges-sharp?branchName=master)](https://dev.azure.com/ap0llo/OSS/_build/latest?definitionId=15&branchName=master)

GhBadgesSharp is a C#/.NET port of the [gh-badges](https://github.com/badges/shields/tree/master/gh-badges)
library that powers [shields.io](https://shields.io).

See [Acknowledgments](#acknowledgments) for license info.

## Installation

GhBadgesSharp is distributed as NuGet package.

- Prerelease and stable builds are available in the
  [GitHub Package Registry](https://github.com/ap0llo/gh-badges-sharp/packages/13795)
- Stable build are also available on [NuGet.org]

## Usage

After installation, a badge can be rendered by calling `Badge.MakeBadge`.
It will return the badge as an SVG image (as `XElement`).

The following code shows how to render this badge:

<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="79.652" height="20">
  <linearGradient id="smooth" x2="0" y2="100%">
    <stop offset="0" stop-color="#bbb" stop-opacity=".1" />
    <stop offset="1" stop-opacity=".1" />
  </linearGradient>
  <clipPath id="round">
    <rect width="79.652" height="20" rx="3" fill="#fff" />
  </clipPath>
  <g clip-path="url(#round)">
    <rect width="37.533" height="20" fill="#555" />
    <rect x="37.533" width="42.119" height="20" fill="#97ca00" />
    <rect width="79.652" height="20" fill="url(#smooth)" />
  </g>
  <g fill="#fff" text-anchor="middle" font-family="DejaVu Sans,Verdana,Geneva,sans-serif" font-size="110">
    <text x="197.665" y="150" fill="#010101" fill-opacity=".3" transform="scale(0.1)" textLength="275.33" lengthAdjust="spacing">Hello</text>
    <text x="197.665" y="140" transform="scale(0.1)" textLength="275.33" lengthAdjust="spacing">Hello</text>
    <text x="575.925" y="150" fill="#010101" fill-opacity=".3" transform="scale(0.1)" textLength="321.19" lengthAdjust="spacing">World</text>
    <text x="575.925" y="140" transform="scale(0.1)" textLength="321.19" lengthAdjust="spacing">World</text>
  </g>
</svg>

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

```bat
  dotnet restore .\src\GhBadgesSharp.sln

  dotnet build .\src\GhBadgesSharp.sln

  dotnet pack .\src\GhBadgesSharp.sln
```

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

Addititional dependencies used for testing:

- [xUnit](http://xunit.github.io/)
- [ApprovalTests](https://github.com/approvals/ApprovalTests.Net)
- [PublicApiGenerator](https://github.com/JakeGinnivan/ApiApprover)

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

## CI Builds

- Continuous integration builds using [Azure Pipelines](https://dev.azure.com/ap0llo/OSS/_build?definitionId=13) run for every
  commit on the `master` branch as well as release- (`release/*`) and feature- (`features/*`) branches.
- Builds of the `master` branch and release branches are published to the [GitHub package registry](https://github.com/ap0llo/gh-badges-sharp/packages/13795).
- Builds of any release branch are also published to NuGet.org (no packages has been published there yet).
- To prevent a commit from triggering a CI build, include `[Skip CI]` in the commit message.
