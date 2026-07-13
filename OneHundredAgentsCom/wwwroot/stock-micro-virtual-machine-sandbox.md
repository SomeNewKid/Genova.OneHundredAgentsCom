# Stock Micro Virtual Machine Sandbox

Sandbox Tester running inside a stock QEMU micro virtual machine asks a simple, uncomfortable question: what can this agent actually touch? It probes the world around it: files, processes, network access, browser use, credentials, hardware, package installation, source control, and other capabilities that matter when an AI agent is not just chatting, but acting.

The useful surprise is that even an ordinary micro virtual machine, with no special hardening, takes a lot off the table. The agent can still work inside its Ubuntu guest. It can run Python, drive Chromium with Playwright, use the network, and produce a structured report. But many powers that would be worrying on a host machine are no longer naturally available. The micro&nbsp;VM becomes a blunt but effective boundary. Not elegant. Not magical. More like putting the agent in a room with fewer doors.

This proof of concept is less about QEMU wizardry than about evidence. It gives a business user a way to compare what an agent can do in a sandboxed runtime instead of relying on cheerful platform claims. The takeaway is practical: isolation changes the conversation before hardening even begins.

::: SIDEBAR :::

Language: Python
Framework: None
Pattern: Single agent
Sandbox: Ubuntu Linux, QEMU
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-microvm-stock" title="Sandbox Report - Local Micro Virtual Machine" :::
