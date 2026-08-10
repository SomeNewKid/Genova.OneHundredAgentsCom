# Takeaway agent Diane

**Takeaway agent Diane** is the fourth experiment in a small CoALA-inspired series about memory in AI agents. Alison used working memory for the current dinner request. Beverly added episodic memory, so previous recommendations and feedback could be recalled. Catherine added semantic memory, distilling repeated episodes into simple facts about the user. Diane adds long-term procedural memory: not just what happened, or what the user tends to like, but how the agent should handle future requests.

The dinner task is deliberately ordinary. A user says something loose, such as “I feel like something classic,” and Diane recommends a takeaway meal. After the recommendation, she looks at the result and the user’s feedback, then decides whether her request-handling skill should change. If the feedback suggests a reusable lesson, she can add a small new rule to her own skill Markdown. That is the interesting bit. The agent is not just remembering the meal; she is adjusting part of her own procedure.

This proof of concept sketches a practical business pattern: agents that improve their operating playbook from experience, without turning every past interaction into a permanent instruction.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [Granite 4.1](https://www.ibm.com/granite)
---

Reference: [CoALA paper](https://arxiv.org/html/2309.02427v3)
Repository: [GitHub](https://github.com/SomeNewKid/TakeawayAgentDiane)

::: /SIDEBAR :::
