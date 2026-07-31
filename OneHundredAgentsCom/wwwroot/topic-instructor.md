# Topic instructor

**Topic instructor** was built as a first pass at CrewAI, using a simple but useful business shape: give it a topic, and it produces a short learning guide. The point is not that the world was crying out for one more blog post generator. The point is to test how CrewAI wants a multi-agent system to be arranged, and whether that arrangement feels useful rather than ceremonial.

The agent uses a small crew with clear roles. A planner sketches the structure, a writer turns that plan into a guide, and an editor reviews the result before it is saved. That division of labour is the interesting part. Instead of one model being asked to “do the whole thing, please, and try not to wander off,” CrewAI gives the work named stages and named responsibilities.

As a proof of concept, **topic instructor** sketches a content workflow that could become more serious: onboarding material, customer education, internal explainers, or first-draft training notes. More importantly, it shows CrewAI’s opinionated style. There is a definite path to follow. For early multi-agent experiments, that is helpful. Fewer philosophical debates, more working parts.

::: SIDEBAR :::

Language: Python
Framework: [CrewAI](https://crewai.com/)
Pattern: Multiple agents
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/TopicInstructor)

::: /SIDEBAR :::
