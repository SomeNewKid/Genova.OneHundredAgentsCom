# Meeting room finder

**Meeting room finder** is a small proof of concept for testing how LangGraph handles a tool-using agent workflow. The user asks for a room in ordinary language: how many people are attending, whether they need a projector, and which weekday they want. The agent turns that request into structured criteria, asks for the right tool checks, and returns the rooms that fit.

The useful part is not the office-room scenario itself. Nobody needed a grand AI ceremony to choose between a few rooms. The point is seeing the tool loop in the open. The model can request a capacity check, a projector check, and a weekday availability check. LangGraph then gives the developer explicit control over how those requests are routed, executed, recorded, and combined into a final result.

That control is the lesson. LangGraph does not hide much of the machinery, which is both refreshing and mildly unforgiving. You can see where the model stops and the workflow begins.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/MeetingRoomFinder)

::: /SIDEBAR :::
