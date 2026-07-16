# Container Sandbox with a Minimized Image

Sandbox Tester running in Docker is a small experiment in making an AI agent’s room smaller and then checking whether it notices. The agent probes its own environment: files, processes, network access, browser use, credentials, tools, and the OpenAI Responses API. The Docker harness runs those probes inside a disposable container and records what was allowed, denied, unavailable, or broken.

The minimized-image profile asks a practical question: if the container already blocks risky tools at runtime, does it still matter whether those tools exist in the image? The answer is yes, but with nuance. In the latest comparison, the headline counts did not move: 255 probe paths were still allowed, 98 denied, 20 errored, and 305 not applicable. That is because the previous profile was already blocking many command families. But the minimized image changes the ground underneath: package managers, SSH/GPG tools, Git, Perl, service tools, and admin helpers are removed or purged where possible, not merely blocked after startup.

This is a useful lesson for business users of AI agents. A sandbox should not rely only on “please do not touch that” signs. A minimized image removes needless handles from the room before the agent arrives.

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
