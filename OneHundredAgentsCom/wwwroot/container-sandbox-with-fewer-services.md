# Container sandbox with fewer services

Sandbox Tester running in a Docker container with fewer ambient services is a proof of concept for a simple question: what can an AI agent actually touch when we stop giving it the usual background machinery of a normal machine? It runs capability probes inside the container, then reports what was allowed, denied, or not available. The point is not to admire the plumbing. The point is to make invisible risk visible.

The Docker setup starts with a container that can still do useful work, including browser automation and an OpenAI Responses API call. Then the ambient-services profile removes or blocks common extras an agent should not casually inherit: service-management tools, SSH and GPG agent paths, D-Bus hints, browser debugging surfaces, broad Linux privileges, and shared host-style IPC. In the comparison run, service-management access and GPG-agent discovery became unavailable while the intended workload still ran.

That is the useful business lesson. Sandboxing is not only about blocking the internet or making files read-only. It is also about removing quiet side doors. Less ambient machinery means fewer surprising ways for an agent to poke the host.

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

::: SANDBOX-REPORT name="sandbox-container-lightweight" title="Sandbox Report - Local Container with Fewer Services" :::
