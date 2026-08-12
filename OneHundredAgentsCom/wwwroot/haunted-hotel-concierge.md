# Haunted hotel concierge

**Haunted hotel concierge** was built to test a very small but useful idea in Microsoft Foundry: can a prompt agent answer from a supplied text resource without needing custom code? The setting is deliberately theatrical. A guest reports something odd, such as knocking inside the wardrobe in Room 14, and the agent checks its haunted-hotel handbook before replying. That makes the test easy to run. If the answer mentions the right room, the right ghost, and the right precaution, the setup is working.

The business shape is more serious than the cobwebs suggest. Many organizations have handbooks, policies, manuals, and operating procedures that staff need to consult quickly. This agent sketches a concierge-style assistant that responds from an approved resource instead of improvising from general knowledge.

The useful lesson is how little machinery is needed to start. Foundry’s managed prompt agent environment can connect a model to a provided file and expose it through chat and an API. Tiny haunted hotel, real platform pattern.

::: SIDEBAR :::

Language: Markdown
Platform: [Microsoft Foundry](https://ai.azure.com/)
Pattern: Prompt agent
Model: [GPT 5 mini](https://developers.openai.com/api/docs/models/gpt-5-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/HauntedHotelConcierge)

::: /SIDEBAR :::
