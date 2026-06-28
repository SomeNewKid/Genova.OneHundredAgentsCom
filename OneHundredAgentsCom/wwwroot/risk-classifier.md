# Risk Classifier

Risk Classifier explores a simple but useful question: can a chatbot help someone turn a messy security conversation into a risk classification? The user describes a discovered vulnerability in ordinary language. The agent asks follow-up questions about exploitability and impact, then settles on two scores: likelihood and severity. Once those are clear, it checks them against a risk matrix and returns a final risk level with a short explanation.

The business shape is familiar. Security and product teams often need to discuss a vulnerability before they can rank it. That discussion is rarely as neat as a form. People add context, correct assumptions, and remember awkward details halfway through. A chat-based agent is a decent fit for that kind of work, provided everyone remembers it is assisting the judgement, not replacing it with magic spreadsheet incense.

The point of the exercise was to take first steps with Microsoft’s Agent Framework. It tests chat sessions, GPT-4o, and local tool use from C# and .NET. The interesting takeaway is not that this small agent solves vulnerability management. It is that Microsoft’s framework appears to offer the same basic agent building blocks as other frameworks, while letting a .NET project stay in its own ecosystem.

::: SIDEBAR :::

Language: C#
Framework: [Agent Framework](https://learn.microsoft.com/agent-framework/)
Pattern: Single agent
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/RiskClassifier)

::: /SIDEBAR :::
