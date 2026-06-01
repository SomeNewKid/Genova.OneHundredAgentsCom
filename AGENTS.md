# Codex Instructions

This file is for Codex agents working in this repository.

## Project

This repository contains the .NET website for "100 Agents in 100 Days". The
website is a catalogue and documentation site for AI agents that live in other
repositories, most of which are expected to be Python projects.

The website itself is an ASP.NET Core/.NET 8 solution. The main website project
is `OneHundredAgentsCom`, and the runnable host project is
`OneHundredAgentsCom.Host`.

## Style

Follow `.editorconfig` and `stylecop.json`. Treat those files as the source of
truth for formatting, analyzer configuration, naming preferences, and file
headers.

Important conventions to remember:

- Use 4 spaces for C# indentation.
- Use 2 spaces for XML project/config files.
- Use file-scoped namespaces for C# files.
- Keep nullable reference types enabled and respect nullability warnings.
- Use the existing Genova project GPL file header for C# source files.
- Prefer explicit types unless the existing code makes the type immediately
  apparent.
- Use `System.Text.Json` and typed models for structured JSON data.
- Prefer existing project patterns over new abstractions.
- Keep changes narrowly scoped to the requested behavior.
- Add comments only where they clarify non-obvious logic.

## Verification

After non-trivial code changes, run the regular verification set:

```powershell
dotnet build Genova.OneHundredAgentsCom.sln --configuration Release
dotnet test OneHundredAgentsCom.UnitTests\OneHundredAgentsCom.UnitTests.csproj --configuration Release
dotnet test OneHundredAgentsCom.QualityTests\OneHundredAgentsCom.QualityTests.csproj --configuration Release
```

The expected standard is no build errors and no new warnings. Investigate any
messages that appear relevant to the change.

If package restore is blocked by local NuGet state but packages are already
restored, a no-restore build is acceptable for local verification:

```powershell
dotnet build Genova.OneHundredAgentsCom.sln --configuration Release --no-restore
```

The integration tests in `OneHundredAgentsCom.IntegrationTests` and the scanner
tests in `OneHundredAgentsCom.ScannerTests` are heavier checks. Run them only
when the change affects routing, HTTP behavior, generated HTML, sitemap output,
security headers, crawling/scanning behavior, or when specifically requested.

## Unit Tests

The unit test project should mirror the folder structure of the main
`OneHundredAgentsCom` class library. For example, tests for a class in
`OneHundredAgentsCom\Models` should live under the corresponding `Models`
folder in `OneHundredAgentsCom.UnitTests`.

Name unit test classes by appending `_Tests` to the target class name. For
example, tests for `AgentCatalogue` belong in `AgentCatalogue_Tests`.

Name unit test methods using snake_case with these casing rules:

- Use sentence casing, so the first word starts with an upper-case letter.
- Preserve proper casing for class names, class members, company names, product
  names, and other proper nouns.

Example:

```csharp
public void AgentCatalogue_should_have_a_group_for_the_OpenAI_Agents_SDK()
```

## Git And Files

Do not delete or revert user changes unless the user explicitly asks. The
working tree may contain generated files such as `bin`, `obj`, `.vs`, and test
results; avoid touching them unless a task specifically requires it.
