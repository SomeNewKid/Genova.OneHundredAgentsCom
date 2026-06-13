# New York Guide

New York Guide is a small proof of concept built to test first steps with Google's Agent Development Kit. The business idea is simple: a visitor asks what to do in New York, and the agent suggests an activity based on the weather.

What made this worth building was not the travel advice itself. It was seeing how quickly ADK could connect a model to local tools and let the agent decide when to use them. The agent checks the weather, then asks for either a good-weather or bad-weather activity. If the weather points outdoors, it might suggest Brooklyn Bridge or a market. If rain appears, it moves indoors. Sensible enough, and unlikely to start a tourism empire by lunchtime.

::: SIDEBAR :::

Language: Python
Framework: [Agent Development Kit](https://adk.dev/)
Pattern: Single agent
Model: [Gemini 3.5 Flash](https://ai.google.dev/gemini-api/docs/models/gemini-3.5-flash)
---

Repository: [GitHub](https://github.com/SomeNewKid/NewYorkGuide)

::: /SIDEBAR :::
