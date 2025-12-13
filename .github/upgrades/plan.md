# Upgrade Plan: .NET 10 Migration

## Table of Contents

- [1 Executive Summary](#executive-summary)
- [2 Migration Strategy](#migration-strategy)
- [3 Detailed Dependency Analysis](#detailed-dependency-analysis)
- [4 Project-by-Project Plans](#project-by-project-plans)
- [5 Package Update Reference](#package-update-reference)
- [6 Breaking Changes Catalog](#breaking-changes-catalog)
- [7 Testing & Validation Strategy](#testing--validation-strategy)
- [8 Risk Management](#risk-management)
- [9 Complexity & Effort Assessment](#complexity--effort-assessment)
- [10 Source Control Strategy](#source-control-strategy)
- [11 Success Criteria](#success-criteria)
- [12 Appendices](#appendices)

---

## 1 Executive Summary

**Selected Strategy**: All-At-Once Strategy - All projects upgraded simultaneously in a single coordinated operation.

**Scope**: All projects in the solution (3 projects):
- `idee5.AspNetCore.csproj` (ClassLibrary)
- `idee5.AspNetCore.TestApi.csproj` (AspNetCore app)
- `idee5.AspNetCore.Tests.csproj` (Test project)

**Current state**: All projects target `net8.0` (SDK-style projects). Assessment indicates no package incompatibilities and no API-level incompatibilities detected.

**Target state**: All projects target `net10.0` (.NET 10 LTS). All referenced NuGet packages remain compatible per assessment.

**Rationale for All-At-Once**:
- Solution size: small (3 projects, ~677 LOC) ? suitable for an atomic upgrade.
- Low complexity: no incompatible NuGet packages reported, SDK-style projects, limited surface area.
- Team can accept short coordinated upgrade and run full-solution tests after the atomic change.

**Key risks**:
- Compilation or API breakages not detected by static analysis may appear during build.
- Test failures introduced by behavioral changes in framework updates.
- Any hidden transitive dependency requiring package upgrades.

---

## 2 Migration Strategy

- Approach: All-At-Once (atomic upgrade). All project file TargetFramework values and package references will be updated in a single coordinated change set.

- Preconditions:
  - Developer workstation has .NET 10 SDK installed and `global.json` (if present) aligns with the SDK.
  - Working branch: `upgrade-to-NET10` (created and active).
  - All pending working-tree changes saved (committed/stashed) before starting the atomic change.

- High-level phases (for human understanding):
  - Phase 0: Preparation (SDK validation, branch, backups)
  - Phase 1: Atomic Upgrade (project TFMs + package updates)
  - Phase 2: Build & Fix (restore, build, fix compilation errors)
  - Phase 3: Test Validation (execute unit/integration tests)
  - Phase 4: Finalize (single commit, PR, code review, merge)

---

## 3 Detailed Dependency Analysis

- Total projects: 3
- Dependency summary (from assessment):
  - `idee5.AspNetCore.csproj` — Class library, depended on by one project.
  - `idee5.AspNetCore.TestApi.csproj` — ASP.NET Core test application (depends on the class library).
  - `idee5.AspNetCore.Tests.csproj` — Test project (depends on the class library and possibly TestApi)

- Dependency ordering (informational): leaves first, roots last. For All-At-Once this is informational only; all projects are changed simultaneously.

- Circular dependencies: None reported.

---

## 4 Project-by-Project Plans

### `idee5.AspNetCore.csproj` (ClassLibrary)

**Current State**: TargetFramework `net8.0`, SDK-style. 13 files, 455 LOC.

**Target State**: TargetFramework `net10.0`.

**Migration Steps (to be executed as part of the atomic upgrade):**
1. Update project file: set `<TargetFramework>net10.0</TargetFramework>` (or append `net10.0` if multi-targeting is required by policy).
2. Check for Directory.Build.props/targets or `Directory.Packages.props` that may override package versions or TFMs and update them consistently.
3. Restore packages and build solution to surface compilation issues.
4. Resolve compilation errors (refer to §6 Breaking Changes Catalog for likely issues).
5. Validate public API behavior where applicable.

**Packages referenced (from assessment)**:
- `idee5.Common` 4.0.0 (compatible)
- `idee5.Common.Data` 2.1.1 (compatible)

**Expected Breaking Changes**: None identified by static analysis. Treat as potential: obsolete APIs, minor behavior changes.

**Validation**:
- Solution builds without errors.
- Dependent projects build successfully.

---

### `idee5.AspNetCore.TestApi.csproj` (ASP.NET Core application)

**Current State**: TargetFramework `net8.0`, SDK-style.

**Target State**: TargetFramework `net10.0`.

**Migration Steps (atomic upgrade)**:
1. Update project file TargetFramework to `net10.0`.
2. Check Program.cs startup patterns for deprecated hosting model changes (not expected but verify compilation).
3. Restore packages and build solution; fix compilation errors discovered.
4. Run any available integration or functional tests that exercise the TestApi.

**Expected Breaking Changes**: None flagged by analysis. Manual verification required for middleware/config changes.

**Validation**:
- Application compiles.
- Integration tests (if any) pass.

---

### `idee5.AspNetCore.Tests.csproj` (Test project)

**Current State**: TargetFramework `net8.0`, SDK-style.

**Target State**: TargetFramework `net10.0`.

**Migration Steps**:
1. Update project file TargetFramework to `net10.0`.
2. Update test SDK or test runner package references if assessment suggests (none required per analysis).
3. Restore and run test suite.

**Validation**:
- All unit tests pass.

---

## 5 Package Update Reference

### Packages referenced in solution

| Package | Current Version | Suggested Version | Projects Affected | Notes |
|---|---:|---:|---|---|
| `idee5.Common` | 4.0.0 | (no change) | `idee5.AspNetCore.csproj` | Marked compatible by analysis |
| `idee5.Common.Data` | 2.1.1 | (no change) | `idee5.AspNetCore.csproj` | Marked compatible by analysis |

Notes: Assessment reports packages as compatible with `net10.0`. No immediate package upgrades required. If compilation shows version conflicts, update the specific package to the least required version that supports `net10.0`.

---

## 6 Breaking Changes Catalog

- Static analysis did not flag binary or source-incompatible APIs for the solution.
- Common areas to inspect during the build/fix phase:
  - Obsolete APIs and types removed in .NET 9/10 (log and fix compiler warnings as errors if configured).
  - ASP.NET Core hosting/startup pattern changes (verify `Program.cs` and top-level statements).
  - Any reflection-based code or runtime binding that may behave differently across runtime versions.

?? Requires validation: runtime behavioral differences not detectable by static analysis. Add integration tests or manual smoke tests where coverage is missing.

---

## 7 Testing & Validation Strategy

- Per-Project validation: All projects must build successfully after the atomic upgrade.
- Full-solution validation: Run the complete test suite located in `idee5.AspNetCore.Tests` and any integration tests that exercise `TestApi`.

Test checklist:
- [ ] Solution restore succeeds
- [ ] Solution builds with 0 errors
- [ ] Unit tests pass
- [ ] Integration tests pass (if present)
- [ ] No new security vulnerabilities introduced

Test projects to run:
- `idee5.AspNetCore.Tests.csproj`

---

## 8 Risk Management

- Overall solution risk: Low (small codebase, no incompatible packages reported)

High-risk scenarios and mitigations:
- Hidden API incompatibility during build: Mitigation — fix compilation errors during the atomic upgrade step; keep changes in single commit and document fixes.
- Test failures: Mitigation — run tests and triage failures immediately after atomic upgrade; prioritize fixes for failing tests.

Rollback plan:
- If upgrade proves blocking, revert the atomic commit on `upgrade-to-NET10` and reopen investigation.

---

## 9 Complexity & Effort Assessment

Per-project complexity (relative):
- `idee5.AspNetCore.csproj` — Low
- `idee5.AspNetCore.TestApi.csproj` — Low
- `idee5.AspNetCore.Tests.csproj` — Low

Overall migration complexity: Low. No special refactoring anticipated beyond resolving compilation errors if they appear.

---

## 10 Source Control Strategy

- Use a single atomic upgrade commit containing all project file changes and package version adjustments.
- Branch: `upgrade-to-NET10` (already created).
- Commit message guidance: `chore(upgrade): migrate projects to net10.0` plus short summary of any fixes applied.
- Create a single pull request for code review. Reviewer checklist: build passes, tests pass, no unexpected package version downgrades.

---

## 11 Success Criteria

The migration is complete when:
- All project files target `net10.0`.
- All package updates from assessment applied (none required beyond compatibility confirmation).
- Solution restores and builds with 0 errors.
- All unit tests pass.
- No outstanding security vulnerabilities reported for NuGet packages in use.

---

## 12 Appendices

- Assessment reference: `.github/upgrades/assessment.md`
- Recommended validation commands (for executor only):
  - `dotnet --list-sdks` (verify .NET 10 SDK installed)
  - `dotnet restore` (after changes)
  - `dotnet build --no-restore`
  - `dotnet test` (run tests)

---

*Iteration 1.3 complete — plan skeleton populated with assessment data and All-At-Once strategy justification.*
