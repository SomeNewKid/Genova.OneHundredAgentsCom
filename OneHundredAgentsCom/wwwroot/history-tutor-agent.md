# History Tutor Agent

The History Tutor Agent is a small experiment in using the OpenAI Agents SDK to coordinate a few simple teaching assistants. A child asks one question, and a manager-style agent decides whether it belongs with a history, mathematics, or geography tutor. If the history tutor is asked for a fun fact, it can use a tool to provide one. It is a deliberately modest use of the SDK.

The useful lesson is how quickly the SDK gets from an idea to a working agent shape. Agents, handoffs, and tools are easy to combine, and the same sketch can be tested with the OpenAI Responses API or with local Ollama-hosted models. That matters for business readers because model choice is rarely just a technical preference. Cost, latency, privacy, and quality all tug in different directions.

This proof of concept does not pretend to be a finished tutor. It shows the shape of one: a front door for questions, specialist helpers behind it, and enough flexibility to keep experimenting.

::: SIDEBAR :::

Language: Python
Framework: [OpenAI Agents SDK](https://openai.github.io/openai-agents-python/)
Pattern: Multi-agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Models: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini) | [Granite 3.3](https://www.ibm.com/granite) | [Gwen 3-4b](https://huggingface.co/Qwen/Qwen3-4B)
---

Repository: [GitHub](https://github.com/SomeNewKid/HistoryTutorAgent)

::: /SIDEBAR :::
