# Stock container sandbox

Sandbox Tester running in a stock Docker container is a small attempt to answer a practical question: what can an AI agent actually do when we give it a controlled runtime, but do not harden it yet? It runs a broad set of checks against its own environment and records what was allowed, blocked, irrelevant, or failed. The point is not to admire the machinery. The point is to stop guessing.

The useful surprise is that Docker changes the agent’s world even before extra security work begins. A container does not magically make an agent safe, and this proof of concept does not pretend otherwise. But it does make many privileged host capabilities unavailable by default. The agent gets a Linux workspace, a disposable writable layer, and enough room to run browser-based checks with Playwright and Chromium, while still being separated from much of the Windows host. For a business team considering local AI agents, that matters. It shows the shape of a simple control: run the agent somewhere observable and disposable before trusting it near the furniture.

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

::: SANDBOX-REPORT name="sandbox-container-stock" title="Sandbox Report - Local Container" :::
