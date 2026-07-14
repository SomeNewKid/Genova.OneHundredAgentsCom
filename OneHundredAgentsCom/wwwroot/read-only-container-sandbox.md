# Read-only Container Sandbox

Sandbox Tester running in Docker is a small proof of concept built to answer a very plain question: when an AI agent runs in a container, what can it actually do? It does not trust the brochure version of sandboxing. It runs capability checks from inside the agent’s own environment and reports what is allowed, denied, unavailable, or broken.

The useful part is the comparison. A stock Docker container still leaves an agent with many practical abilities: reading runtime details, writing files, using the network, launching tools, running browser automation, and calling the OpenAI Responses API. The hardened Docker profile then tightens the file system: read-only root, controlled writable areas, read-only denied fixtures, `noexec` writable mounts, and a Landlock path policy. That starts turning broad machine access into a smaller, more deliberate workspace.

For a business reader, the lesson is not that this one setup is magically safe. It is that sandboxing can be measured. Before giving an agent sensitive work, you can ask what it can touch, change, execute, and call. That beats vibes, which remain a poor security architecture.

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

::: SANDBOX-REPORT name="sandbox-container-readonly" title="Sandbox Report - Local Read-only Container" :::
