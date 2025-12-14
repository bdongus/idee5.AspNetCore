# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [%USERPROFILE%\source\repos\idee5.AspNetCore\idee5.AspNetCore.TestApi\idee5.AspNetCore.TestApi.csproj](#%userprofile%sourcereposidee5aspnetcoreidee5aspnetcoretestapiidee5aspnetcoretestapicsproj)
  - [%USERPROFILE%\source\repos\idee5.AspNetCore\idee5.AspNetCore.Tests\idee5.AspNetCore.Tests.csproj](#%userprofile%sourcereposidee5aspnetcoreidee5aspnetcoretestsidee5aspnetcoretestscsproj)
  - [idee5.AspNetCore.csproj](#idee5aspnetcorecsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 3 | All require upgrade |
| Total NuGet Packages | 2 | All compatible |
| Total Code Files | 20 |  |
| Total Code Files with Incidents | 3 |  |
| Total Lines of Code | 677 |  |
| Total Number of Issues | 4 |  |
| Estimated LOC to modify | 0+ | at least 0,0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [%USERPROFILE%\source\repos\idee5.AspNetCore\idee5.AspNetCore.TestApi\idee5.AspNetCore.TestApi.csproj](#%userprofile%sourcereposidee5aspnetcoreidee5aspnetcoretestapiidee5aspnetcoretestapicsproj) | net8.0 | 🟢 Low | 0 | 0 |  | AspNetCore, Sdk Style = True |
| [%USERPROFILE%\source\repos\idee5.AspNetCore\idee5.AspNetCore.Tests\idee5.AspNetCore.Tests.csproj](#%userprofile%sourcereposidee5aspnetcoreidee5aspnetcoretestsidee5aspnetcoretestscsproj) | net8.0 | 🟢 Low | 1 | 0 |  | DotNetCoreApp, Sdk Style = True |
| [idee5.AspNetCore.csproj](#idee5aspnetcorecsproj) | net8.0 | 🟢 Low | 0 | 0 |  | ClassLibrary, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 2 | 100,0% |
| ⚠️ Incompatible | 0 | 0,0% |
| 🔄 Upgrade Recommended | 0 | 0,0% |
| ***Total NuGet Packages*** | ***2*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| idee5.Common | 4.0.0 |  | [idee5.AspNetCore.csproj](#idee5aspnetcorecsproj) | ✅Compatible |
| idee5.Common.Data | 2.1.1 |  | [idee5.AspNetCore.csproj](#idee5aspnetcorecsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;idee5.AspNetCore.csproj</b><br/><small>net8.0</small>"]
    P2 --> P3
    P3 --> P1
    click P1 "#idee5aspnetcorecsproj"

```

## Project Details

<a id="idee5aspnetcorecsproj"></a>
### idee5.AspNetCore.csproj

#### Project Info

- **Current Target Framework:** net8.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 13
- **Number of Files with Incidents**: 1
- **Lines of Code**: 455
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
    end
    subgraph current["idee5.AspNetCore.csproj"]
        MAIN["<b>📦&nbsp;idee5.AspNetCore.csproj</b><br/><small>net8.0</small>"]
        click MAIN "#idee5aspnetcorecsproj"
    end
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

