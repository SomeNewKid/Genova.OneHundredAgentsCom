# Bedtime Story Teller

Bedtime Story Teller is a small proof of concept built to test guardrails in the OpenAI Agents SDK. It takes a child's story request, asks a tool for story ingredients, and returns a short tale in plain language.

The project explores guardrails before the agent responds, after it drafts an answer, before it calls a tool, and after the tool returns information. Those checks can shape what happens next, or stop the run entirely when the request or intermediate result is outside the intended use. That matters for business systems because the risky moment is not always the first user message. Sometimes it appears when the agent chooses how to use supporting tools.

This sketch points toward safer agentic applications: not perfect safety, not magic judgement, but practical checkpoints placed where decisions happen.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/BedtimeStoryTeller)

::: /SIDEBAR :::
