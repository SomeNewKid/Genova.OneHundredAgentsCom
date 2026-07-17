# Sandbox Agent

Sandbox Agent explores a simple but important question: where should an AI agent be allowed to work? In this experiment, the agent is asked to generate a basic HTML lesson, serve it as a web page, and capture a screenshot. That is a small task, but it has the right shape: create something, run it, inspect the result, and leave behind artifacts a human can review.

The interesting part is not the web page. The interesting part is the container around the agent. Sandbox Agent runs inside a purpose-built Docker environment, with its work directed into a disposable run folder. The host starts the job, but the agent itself is expected to do its work inside the sandbox. That keeps the experiment focused on a practical concern: giving an agent enough room to be useful without casually handing it the whole machine and a polite note saying “please behave.”

For a business reader, this sketches a safer pattern for agentic work. An agent could draft, render, test, or package content in an isolated workspace, then return outputs for review. It is not a finished security model, but it is a concrete step toward treating AI work areas as bounded places.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Sandbox: Ubuntu container, Docker
Model: [GPT-5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SandboxAgent)

::: /SIDEBAR :::
