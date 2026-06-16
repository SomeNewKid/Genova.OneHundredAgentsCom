# Document Summarizer

Document Summarizer is a small proof of concept built to test guardrails around an agent based on IBM’s BeeAI framework. The business task is deliberately plain: a user asks for a summary of a file, and the agent decides when to call a document-reading tool before producing a short answer. That simplicity is the point. It keeps the moving parts visible, without burying the experiment under a grand document-intelligence costume.

The useful part is not that the agent can summarize a file. The useful part is where the project adds control. It checks the user request before the agent starts, checks messages before they go to the model, checks model responses, checks tool arguments before a file is read, checks tool output before the agent uses it, and checks the final answer before printing. Those are practical pressure points for business systems that need agents to use information carefully. The project sketches how BeeAI can help shape not only what an agent can do, but what it is allowed to see, say, and pass along.

::: SIDEBAR :::

Language: Python
Framework: [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Single agent
Model: [Granite 3.3](https://www.ibm.com/granite)
---

Repository: [GitHub](https://github.com/SomeNewKid/DocumentSummarizer)

::: /SIDEBAR :::
