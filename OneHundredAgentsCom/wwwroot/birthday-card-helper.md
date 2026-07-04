# Birthday Card Helper

Birthday Card Helper is a deliberately small agent built to test a larger question: can the Otto Agent framework make new agents easier to create? The business task is modest, almost aggressively so. The agent finds the next person with a birthday, looks up that person’s interests, and asks a model to write a short message suitable for a card. Nobody is pretending this is the future of greeting cards.

The useful work here is in the framework shape around the agent. Earlier Otto Agent examples needed more application code to wire together skills, tools, model calls, and the harness loop. This project trims that down. A reusable skilled-agent pattern handles more of the common model-backed decision flow, while simple function-based tools reduce the ceremony needed to give the agent controlled actions.

The result is a cleaner proof of concept. The birthday message is the visible output, but the real lesson is that the next Otto Agent application should take less effort to assemble. That matters more than whether Sue gets a card which makes her smile.

::: SIDEBAR :::

Language: Python
Framework: Bespoke Otto framework
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-4.1 mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---
Repository: [GitHub](https://github.com/SomeNewKid/BirthdayCardHelper)

::: /SIDEBAR :::
