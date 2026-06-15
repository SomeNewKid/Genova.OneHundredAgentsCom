# Internal Acronym Explainer

Internal Acronym Explainer is a small proof of concept for testing guardrails in Google's Agent Development Kit&nbsp;(ADK). It looks like a modest business helper: ask what an acronym means, and it separates company meanings from common ones. The useful part is not the acronym table, which is exactly as glamorous as it sounds. It is the control points around the agent.

The project tries ADK callbacks before and after the agent runs, before and after tool calls, and before and after model calls. Those callbacks can block a request, filter tool results, rewrite a tool call, or replace an unsafe answer before it reaches the user. That gives a business team a way to turn policy into executable checks instead of polite suggestions buried in a prompt. The catch is worth noticing: if a rule is only phrased as an instruction, some models may treat it as optional advice. Guardrails are the more serious machinery, and this little agent shows where that machinery can bite.

::: SIDEBAR :::

Language: Python
Framework: [Agent Development Kit](https://adk.dev/)
Pattern: Single agent
Models: [Gemini 3.5 Flash](https://ai.google.dev/gemini-api/docs/models/gemini-3.5-flash) | [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o) | [Granite 3.3](https://www.ibm.com/granite)
---

Repository: [GitHub](https://github.com/SomeNewKid/InternalAcronymExplainer)

::: /SIDEBAR :::
