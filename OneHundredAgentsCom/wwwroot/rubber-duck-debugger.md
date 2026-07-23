# Rubber duck debugger

**Rubber duck debugger** is a small proof of concept built to test human-in-the-loop approvals with the OpenAI Agents SDK. The business sketch is familiar enough: a developer describes a bug, the agent asks a few clarifying questions, and a structured bug report takes shape. The important part is not that the agent can chat. Chatbots are not exactly an endangered species. The important part is that the agent stops before taking the action that matters.

In this case, that action is creating the bug report. Once the agent has enough detail, it prepares the report and tries to call a tool that saves it. The OpenAI Agents SDK pauses that tool call and waits for the human to approve or reject it. If the report needs changes, the agent revises it and asks again. If the human approves, the report is saved and the session ends.

This shows a simple but useful pattern: agents do not have to be either powerless assistants or unchecked automation. They can prepare work, explain what they are about to do, and wait at the edge where human judgment still belongs.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Chatbot agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/RubberDuckDebugger)

::: /SIDEBAR :::
