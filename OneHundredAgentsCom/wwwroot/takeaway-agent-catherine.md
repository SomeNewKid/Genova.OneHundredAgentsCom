# Takeaway agent Cathernine

**Takeaway agent Cathernine** is a small proof of concept for testing long-term semantic memory, following ideas from the CoALA paper. The user gives a simple dinner hint, such as wanting a spicy pizza or a vegetarian curry, and the agent recommends a takeaway meal. Afterward, feedback from the user becomes part of the agent’s memory.

The interesting bit is not the meal itself. It is how Catherine turns repeated episodes into more durable facts about the user. One good pizza recommendation is just an episode. Two or more positive pizza experiences can become a semantic memory: the user likes pizza. That fact can then help shape future recommendations, even when the exact earlier meals are no longer the focus. Episodic memory supplies the evidence; semantic memory keeps the reusable lesson.

This sketches a practical business capability: a recommendation assistant that gets a little less blank each time someone uses it. It does not need a grand customer profile or a sprawling personalization platform to be useful. It tests a modest idea: remembered feedback can become simple, portable facts that make later suggestions feel more personal.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [Granite 4.1](https://www.ibm.com/granite)
---

Reference: [CoALA paper](https://arxiv.org/html/2309.02427v3)
Repository: [GitHub](https://github.com/SomeNewKid/TakeawayAgentCathernine)

::: /SIDEBAR :::
