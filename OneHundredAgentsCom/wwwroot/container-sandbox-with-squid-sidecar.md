# Container Sandbox with Squid Sidecar

Sandbox Tester running in Docker is a small experiment in making an AI agent less mysterious. It runs a battery of capability checks inside a container and asks a blunt question: what can this process actually do? Can it read files, write files, launch programs, use Chromium through Playwright, call the OpenAI Responses API, reach random websites, talk to package registries, or leak data over odd network paths?

The Docker version starts with ordinary container isolation, then adds harder boundaries. A read-only filesystem profile removes many easy file and execution paths while still leaving enough room for the tester, browser, and output files to work. The newer network-egress profile pairs the sandbox container with a Squid proxy sidecar. The sandbox can only reach the outside world through that gateway, where allowed domains can be listed and other traffic can be refused. It is not magic. It is a bouncer with a clipboard, which is often better than vibes.

For a business reader, the useful idea is control. This proof of concept shows how an AI agent can still do approved work while losing many privileged capabilities it never needed.

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

::: SANDBOX-REPORT name="sandbox-container-squid" title="Sandbox Report - Local Container with Squid Sidecar" :::
