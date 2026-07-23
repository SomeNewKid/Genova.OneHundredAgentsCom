# Accessible HTML coder

**Accessible HTML coder** explores a practical question: can LangGraph guide a model through a task where “write the thing” is not enough? The agent starts with a simple requirement for a web page, asks GPT-4o to create the HTML, then sends the result through a reviewer. If the reviewer finds accessibility problems, the coder gets the feedback and tries again.

The interesting part is the shape of the work. LangGraph gives the project a clear loop: generate, check, revise, stop. The model still produces the creative output, but the workflow around it is deterministic. That matters. It means the agent is not just asked nicely to do better; it is placed inside a process that keeps score.

As a business sketch, this points toward automated systems that can review and correct their own work before handing it back to a person. This project is not a production system. It is a compact experiment showing how LangGraph can turn model output into an iterative, inspectable workflow.

::: SIDEBAR :::

Language: Python
Framework: [LangGraph](https://www.langchain.com/langgraph)
Pattern: Single agent
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/AccessibleHtmlCoder)

::: /SIDEBAR :::
