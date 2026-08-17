# Adventurer Makenzie

**Adventurer Makenzie** was built less as a game expert and more as a small observability test. It sketches a simple customer-facing interaction: a player names an adventure game she liked, and Makenzie recommends another game in the same broad style. Here, the recommendation is mostly a stage prop. The interesting question is whether we can see what the agent did.

The project uses Microsoft’s Agent Framework to test that question. A run produces traces for the agent call, the model calls, the tool choice, and custom application spans. Those traces flow through OpenTelemetry, so they can be inspected in tools such as Jaeger. 

The useful lesson is modest but important. If an agent will run many times, tracing should not be decoration added later. It is how you troubleshoot odd choices, measure cost, and find places to improve. Microsoft’s Agent Framework gives a solid starting point, and the sample shows that adding your own trace details is quite approachable.

::: SIDEBAR :::

Language: Python
Framework: [Agent Framework](https://learn.microsoft.com/agent-framework/)
Observability: [OpenTelemetry](https://opentelemetry.io/)
Pattern: Single agent
Model: [GPT-5](https://developers.openai.com/api/docs/models/gpt-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/AdventurerMakenzie)

::: /SIDEBAR :::
