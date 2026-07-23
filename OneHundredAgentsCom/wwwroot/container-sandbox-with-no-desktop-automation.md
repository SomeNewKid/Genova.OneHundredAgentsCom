# Container sandbox with no desktop automation

Sandbox Tester running in a Docker container with a disabled desktop automation channel asks a practical question: if an AI agent is boxed in, what can it still touch? It runs inside the container and checks the environment around it. The result is a capability report, because “sandboxed” can mean anything from “carefully confined” to “we put it in a container and hoped for the best.”

Before this step, the Docker sandbox using network socket control still had 234 allowed probe paths. After disabling the desktop automation channel, the count moved to 229 allowed. Five paths moved away from allowed.

The useful change is small but pointed. The agent no longer sees desktop automation or accessibility surfaces. It can still run the intended workload: Python, Playwright, Chromium screenshots, and the OpenAI API test. But it loses another set of unnecessary senses. For a business testing agent isolation, that is the point: less accidental reach, same intended job.

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

::: SANDBOX-REPORT name="sandbox-container-no-automation" title="Sandbox Report - Local Container with No Desktop Automation" :::
