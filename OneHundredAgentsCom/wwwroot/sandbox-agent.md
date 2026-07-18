# Sandbox Agent

Sandbox Agent explores a practical question: if an OpenAI Agents SDK agent is going to create files, run a local web server, and drive a browser, can we give it a purpose-built place to do that work instead of handing it the whole machine and hoping everyone behaves? The agent generates a simple HTML lesson page, serves it inside its container, and captures a screenshot with Chromium. The web page is not the point. The bounded workspace is.

The interesting part is the declarative sandbox. The workload declares the capabilities it needs, such as network access, OpenAI access, the Agents SDK, and Playwright with Chromium. The sandbox then builds the matching Docker image and container policy. Empty capability lists stay small and tightly closed. Extra capabilities soften the container only where the workload needs it. That is a much calmer model than one giant “AI can do stuff” environment.

As a proof of concept, Sandbox Agent sketches how a business could let an agent produce and inspect a web artifact while keeping the blast radius visible. It is not a finished security product. But it shows a useful pattern: make the agent’s room before inviting the agent in.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Ubuntu container, Docker
Model: [GPT-5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxAgent)

::: /SIDEBAR :::

::: SANDBOX-REPORT name="sandbox-container-declaractive" title="Sandbox Report - Local Minimal, Declarative Container" :::

