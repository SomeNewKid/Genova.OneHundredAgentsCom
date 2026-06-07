# Genova.OneHundredAgentsCom

Genova.OneHundredAgentsCom is a Genova website for **[www.100agentsin100days.com](http://www.100agentsin100days.com)**, presenting agents built as part of the "100 agents in 100 days" project.

> [!WARNING]
> This website is powered by the Genova platform and should not be considered production-ready. It is published as source for review and experimentation rather than as a turnkey website template.

> [!IMPORTANT]
> A fresh public clone of this repository should not be expected to restore or build without additional Genova infrastructure. Many Genova dependencies are distributed through a private authenticated NuGet feed, and the public source does not include feed credentials or a complete public package graph.

## Installation

```bash
dotnet restore
dotnet build
```

Or reference the website library from a Genova host application.

## Usage

Run it through the included host project:

```bash
dotnet run --project OneHundredAgentsCom.Host
```

The host loads the website class and runs it via the Genova engine.

## Features

* Markdown-driven pages for individual agents
* Agent catalogue grouped by framework or purpose
* Static website assets, styles, scripts, icons, and sitemap endpoints
* Custom HTML modifiers for layout, sidebar, and agent navigation
* Host-based multi-tenant website configuration

## Notes

* This project is part of the Genova multi-tenant ASP.NET Core platform.
* It is executed via a host and engine, not as a standalone site.
* The website content is embedded from the `wwwroot` and `Data` folders.

## Third-Party Notices

This project has direct runtime dependencies on third-party NuGet packages, including Microsoft IdentityModel JWT packages (MIT), `Microsoft.ML*` packages (MIT). See each package's NuGet license metadata for full license and notice terms.

## License

GNU General Public License v3.0. See the `LICENSE` file for details.
