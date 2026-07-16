# Container Sandbox with Minimized Image

Sandbox Tester running in a Docker container with resource limits is a proof of concept for a practical worry: an AI agent may not need to break out of a sandbox to cause trouble. Sometimes it only needs to consume too much of the machine it already has. So this version asks a simple question. What can the agent still do when Docker puts a tighter budget around its CPU, memory, process count, and open files?

The agent runs capability probes inside the container and reports what worked, failed, or did not apply. The `resource-limits` profile keeps the earlier Docker hardening: restricted files, controlled network access, fewer commands, fewer services, and a seccomp layer. It then adds explicit resource limits while still allowing Python, Playwright, Chromium, and the OpenAI API probe to run.

The comparison result was calm, which is useful. No tested capability moved from allowed to denied. The agent still did its intended work. But the container now has clearer boundaries around how much of the host it can use. That points toward a sensible pattern: do not just ask what an agent can access. Ask how much damage it can do by being busy.

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

::: SANDBOX-REPORT name="sandbox-container-minimized" title="Sandbox Report - Local Container with a Minimized Image" :::
