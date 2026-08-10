# Takeaway agent Alison

**Takeaway agent Alison** is a small proof of concept for exploring memory in AI agents, following the CoALA paper. The business task is deliberately familiar: a user gives a loose dinner hint, such as “a vegetarian pizza” or “a spicy curry with paneer,” and Alison recommends a nearby takeaway meal. It uses LangGraph to keep the current request, menu observations, candidate meals, and final choice in working memory for one run.

The point is not to build the world’s most ambitious food app. Mercifully, no one needs another app that turns dinner into a governance framework. Alison is a foundation project: synthetic shops, synthetic menus, and a focused agent loop that shows what an agent can do when it has only short-term context. It can still ask useful questions of its current situation, narrow the options, and return a sensible recommendation.

What it cannot do yet is learn. If Alison recommends paneer tonight, it will not remember tomorrow whether that was a triumph or a regrettable dairy incident. That is the useful boundary. Working memory is enough to act correctly now, but longer-term memory is what could make the agent more personal over time.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [Granite 4.1](https://www.ibm.com/granite)
---

Reference: [CoALA paper](https://arxiv.org/html/2309.02427v3)
Repository: [GitHub](https://github.com/SomeNewKid/TakeawayAgentAlison)

::: /SIDEBAR :::
