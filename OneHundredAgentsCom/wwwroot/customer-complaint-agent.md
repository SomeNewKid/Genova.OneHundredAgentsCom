# Customer complaint agent

The **customer complaint agent** was built to test a boring but useful question: when an AI agent touches a real business decision, who is allowed to do what? This proof of concept gives the agent an email ID and asks it to work through a refund request. It checks the email, finds the related order and product, verifies whether a photo shows damage, and applies a refund policy. A low-cost damaged item can be marked for refund. A more expensive one is escalated. Missing evidence, a missing order, or an already-refunded purchase stops the process instead of letting the agent bluff its way through customer service theatre.

The interesting part is the split of responsibilities. The agent proposes structured decisions. The harness owns the loop, validates those decisions, runs approved tools, and records state changes. The domain layer provides controlled access to customer data, orders, products, attachments, and refund rules. That separation is the point. It sketches how an agent can be useful without being handed the keys to the shop, the till, and the apology email all at once.

::: SIDEBAR :::

Language: Python
Framework: None
Pattern: Single agent
Integration: [OpenAI Responses API](https://platform.openai.com/docs/api-reference/responses)
Model: [GPT-5.4 mini](https://developers.openai.com/api/docs/models/gpt-5.4-mini)
---
Repository: [GitHub](https://github.com/SomeNewKid/CustomerComplaintAgent)

::: /SIDEBAR :::
