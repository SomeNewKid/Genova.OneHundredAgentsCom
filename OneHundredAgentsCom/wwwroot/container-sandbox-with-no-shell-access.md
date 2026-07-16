# Container Sandbox with No Shell Access

Sandbox Tester running in a Docker container with no shell access tests a blunt but useful idea: what happens when an AI agent can still use Python tools, but cannot casually ask the operating system to run commands? This matters because shell access is often the side door. If an agent can call `sh`, `curl`, `ps`, package managers, or helper commands, the sandbox may be doing less work than the diagram suggests.

Before this step, the `persistence-control` profile still had 67 shell probes and 66 alternate-shell probes marked as allowed. After `no-shell-access`, both numbers dropped to zero.

The useful part is that the agent did not simply die in a corner. The run completed, the report was written, and the Python-side Playwright screenshot still worked. This profile sketches a tighter agent runtime: Python remains available for intended work, while the easy escape hatch of “just run a command” is closed.

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

::: SANDBOX-REPORT name="sandbox-container-no-shell" title="Sandbox Report - Local Container with No Shell Access" :::
