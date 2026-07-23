# Greeting card writer

**Greeting card writer** explores a small but useful pattern: one manager agent deciding which specialist agent should handle a customer-style request. The user describes the reason for sending a card, and the main agent routes the job to a birthday, get-well, or sympathy writer. That specialist then drafts the message and returns a finished card with the recipient, sender, and note in place.

The business shape is easy to recognise. A company might receive many requests that sound similar on the surface, but need different tone, wording, and judgement once you look closer. “Happy birthday” and “I’m sorry for your loss” should not be handled by the same cheerful blob of text generation. That is where the manager-worker structure matters.

This proof of concept was built to test how simple that handoff can be in Google’s Agent Development Kit. The interesting part is not that it writes cards. The interesting part is that ADK lets the coordinator work with named sub-agents, each with its own job. It is a compact way to try delegated work without building a whole office around it.

::: SIDEBAR :::

Language: Python
Framework: [Agent Development Kit](https://adk.dev/)
Pattern: Multiple agent
Model: [Gemini 3.5 Flash](https://ai.google.dev/gemini-api/docs/models/gemini-3.5-flash)
---

Repository: [GitHub](https://github.com/SomeNewKid/GreetingCardWriter)

::: /SIDEBAR :::
