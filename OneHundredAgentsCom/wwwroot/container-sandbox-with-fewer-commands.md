# Container sandbox with fewer commands

Sandbox Tester running in a Docker container with fewer available command families is a proof of concept for a very practical question: if an AI agent is put in a container, what can it still do? The agent runs a broad set of capability checks and reports which actions are allowed, denied, unavailable, or broken. That makes the sandbox less of a comforting diagram and more of a receipt.

This version focuses on the `execution-control` profile. Earlier Docker hardening limited file access, network egress, and ambient services. This step asks a narrower question: what happens when needless command families are disabled? Package managers, source-control tools, admin commands, namespace tools, extra shells, and process helpers are blocked while Python, Playwright, Chromium, and the OpenAI API test still run.

The result is not a claim that the container is magically safe. Sensible people do not say that near computers. It does show that removing commands changes the agent’s practical reach.

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

::: SANDBOX-REPORT name="sandbox-container-tightened" title="Sandbox Report - Local Container with Fewer Commands" :::
