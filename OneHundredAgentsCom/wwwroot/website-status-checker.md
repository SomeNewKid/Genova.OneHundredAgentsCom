# Website status checker

**Website status checker** was built to test a middle path for AI agents. Instead of starting from a large agent framework, it uses Otto, a small first-party framework that keeps the agent and the harness in separate lanes. The agent decides what should happen next. The harness controls the loop, validates decisions, runs tools, records evidence, and decides when the job is finished. That split is the main lesson here.

The business task is simple and familiar: check whether a known website looks healthy. The agent connects to a URL, gathers browser evidence, captures a screenshot, and returns a structured result such as healthy, unhealthy, or unknown. It sketches the kind of check a support, operations, or monitoring team might want before deciding whether a page needs attention. It is not trying to replace proper monitoring. It is more like a careful assistant with a browser and a clipboard.

The useful part is not only the website check. It is the experiment in building just enough framework to support this agent and future ones. Otto sits between “write everything from scratch every time” and “adopt a full vendor framework on day one.” That feels like a good place to learn what is actually needed, without inviting the whole circus into the tent.

::: SIDEBAR :::

Language: Python
Framework: Bespoke Otto framework
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---
Repository: [GitHub](https://github.com/SomeNewKid/WebsiteStatusChecker)

::: /SIDEBAR :::
