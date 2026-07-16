# Container Sandbox with Runtime Control

Sandbox Tester running in Docker is a small proof of concept for a very practical question: if an AI agent can run Python, browse with Chromium, use Playwright, and call the OpenAI Responses API, what else can it do while it is in there? The agent probes its own container and records which actions are allowed, denied, errored, or not relevant.

The latest Docker profile adds runtime control. Before this step, the hardened Docker sandbox still allowed 255 probe paths. After runtime control, that fell to 240. Fifteen previously allowed paths moved to denied or error. The important workload still worked: Playwright captured screenshots, Chromium ran, and the OpenAI API call succeeded.

The useful lesson is narrow but real. The sandbox now limits Python’s ability to extend itself while it is running. It blocks package installation, newly written Python scripts, imports from writable directories, and most direct native-library or operating-system API access. That does not make the container magic. It does make the boundary less trusting, which is where sandboxing starts to get interesting.

::: SIDEBAR :::

Language: Python
Framework: None
Pattern: Single agent
Sandbox: Ubuntu container, Docker
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-container-controlled" title="Sandbox Report - Local Container with Runtime Control" :::
