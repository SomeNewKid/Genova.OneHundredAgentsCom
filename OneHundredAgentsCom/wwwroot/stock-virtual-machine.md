# Stock Virtual Machine

Sandbox Tester running on a virtual machine asks a more useful question than “is this safe?” It asks what the agent can actually do from inside a stock VM with no special hardening. The agent probes its surroundings and reports which capabilities are available, blocked, missing, or not relevant. That includes practical areas a business would care about: files, processes, credentials, network access, browser state, cloud tools, databases, hardware, scheduling, and logs.

The interesting result is not that the VM is magic. It is not. A plain virtual machine is still a computer, and the agent can still do plenty inside it. But many powers that feel normal on a user’s own desktop become unavailable or empty. Host browser profiles are not just lying around. Personal environment secrets are not automatically present. Hardware and local account access look different. The agent is still busy, but it is no longer casually wearing the user’s house keys.

This proof of concept points toward a simple business habit: test the boundary, don’t assume it. Running an agent in a VM does not make it secure by proclamation, but it can remove a surprising amount of accidental privilege before anyone starts polishing policy documents.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Ubuntu Linux, VirtualBox
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxTester)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-vm-stock" title="Sandbox Report - Local Virtual Machine" :::
