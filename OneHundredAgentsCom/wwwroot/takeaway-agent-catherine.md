# Takeaway agent Cathernine

**Takeaway agent Cathernine** is the third experiment in a small CoALA-inspired series about memory in AI agents. Alison worked only with the current dinner request. Beverly added episodic memory, so previous recommendations and feedback could be recalled. Catherine adds long-term semantic memory: not just remembering what happened, but distilling repeated episodes into simple facts about the user.

The user gives a short dinner hint, such as wanting a spicy pizza or a vegetarian curry, and Catherine recommends a takeaway meal. After the user gives feedback, that interaction becomes part of the agent’s remembered history. If the pattern repeats, Catherine can turn those episodes into durable preferences, such as the user liking pizza, spicy meals, or a particular restaurant. One happy dinner does not become a personality profile. Two or more related signals start to look like a fact.

This sketches a useful business capability: recommendations that become more personal without needing a giant customer-data machine humming ominously in the corner. The agent still makes a fresh recommendation each time, but semantic memory gives it a small, reusable sense of what has mattered before.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [Granite 4.1](https://www.ibm.com/granite)
---

Reference: [CoALA paper](https://arxiv.org/html/2309.02427v3)
Repository: [GitHub](https://github.com/SomeNewKid/TakeawayAgentCathernine)

::: /SIDEBAR :::
