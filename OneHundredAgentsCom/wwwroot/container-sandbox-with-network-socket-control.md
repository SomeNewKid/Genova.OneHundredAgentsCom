# Container sandbox with network socket control

Sandbox Tester running on a Docker container with network socket control is a proof of concept for a simple question: what can an AI agent actually do once it is placed inside a hardened sandbox? The tester runs capability probes from inside the container and records whether actions are allowed, denied, unavailable, or broken. The point is not to admire the container. The point is to catch the awkward truth before a real agent does.

This version focuses on network escape paths. The previous Docker profile already blocked many filesystem, process, browser, package, and runtime behaviours while still allowing Python, Playwright, Chromium, and the OpenAI Responses API to work. Adding network socket control moved the report from 240 allowed probe paths to 234 allowed. Six more paths were closed without breaking the core agent workload.

The new denials are concrete. The agent can no longer use Python to send UDP traffic, reach cloud-style metadata endpoints, listen on every network interface, or bind privileged low-numbered ports. It can still use normal allowed web access through the proxy. That is the useful shape of the experiment: fewer quiet side doors, same intended work.

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

::: SANDBOX-REPORT name="sandbox-container-controlled-sockets" title="Sandbox Report - Local Container with Network Socket Control" :::
