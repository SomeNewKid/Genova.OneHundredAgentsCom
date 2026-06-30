# Movie Suggester

Movie Suggester is a small CrewAI proof of concept built to test how guardrails behave in a multi-agent workflow. The user asks for a kind of film, such as a love story or a scary movie. One agent turns that request into a genre, another selects a matching film, and a third writes the recommendation back in plain language.

The interesting part is not the movie suggestion. Nobody needed a tiny committee to pick a Friday-night comedy. The point is control. The project tests guardrails before the crew runs, after an agent responds, before and after tool calls, and around model calls. Those checks can block unwanted input, restrict agent choices, filter tool usage, and shape final responses.

For a business reader, this sketches a useful pattern: agents can be guided at several points in a process, not just prompted once and trusted forever. That matters when a workflow needs policy boundaries, content rules, or escalation paths. It also shows the limit of the idea. Guardrails help steer behaviour, but they are not a substitute for sandboxing, permissions, audit trails, and other boring safeguards that keep real systems upright.

::: SIDEBAR :::

Language: Python
Framework: [CrewAI](https://crewai.com/)
Pattern: Multi-agent
Model: [GPT-4.1-mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/MovieSuggester)

::: /SIDEBAR :::
