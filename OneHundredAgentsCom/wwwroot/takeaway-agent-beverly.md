# Takeaway agent Beverly

**Takeaway agent Beverly** is the second step in a small series of memory experiments based on the CoALA paper. The earlier Alison agent used only working memory: it could handle the current dinner request, but yesterday might as well have happened in another universe. Beverly adds long-term episodic memory, so each recommendation can become part of a remembered history.

The business sketch is simple. A user asks for dinner, such as a vegetarian pizza or a spicy curry, and Beverly recommends a takeaway meal. The user can also give feedback, like “That was really great” or “That was too spicy.” Beverly stores the meal and feedback as an episode, then uses recent episodes when making later recommendations. Positive feedback can make a meal more likely to return. Negative feedback can help avoid repeating a bad call. A request for “something different from last time” can use even neutral history.

This is not a takeaway product dressed up in serious shoes. It is a proof of concept for a practical capability: using user feedback to turn isolated agent runs into a lightweight preference history.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [Granite 4.1](https://www.ibm.com/granite)
---

Reference: [CoALA paper](https://arxiv.org/html/2309.02427v3)
Repository: [GitHub](https://github.com/SomeNewKid/TakeawayAgentBeverly)

::: /SIDEBAR :::
