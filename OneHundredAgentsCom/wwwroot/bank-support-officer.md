# Bank Support Officer

Bank Support Officer is a small proof of concept built to test Pydantic AI in a familiar setting: a customer asks a bank for help, and the agent replies with support advice. The example covers two common moments in banking support. A customer can ask for an account balance, or report a lost card. The agent can look up customer context, call a balance-checking tool when needed, and return advice that includes whether the card should be blocked.

The useful part is that Pydantic AI keeps the agent’s inputs and outputs strongly shaped. The response is not just a blob of cheerful text. It has expected fields: the advice, a card-blocking decision, and a risk score. That matters because business systems usually need decisions they can inspect, route, log, or refuse to trust.

This agent was built as a first step with Pydantic AI, and it makes a good case for the framework’s appeal. If you like Python agents but prefer typed contracts over vibes in a trench coat, this is a sensible place to start.

::: SIDEBAR :::

Language: Python
Framework: [Pydantic AI](https://ai.pydantic.dev/)
Pattern: Single agent
Model: [GPT-5.2](https://developers.openai.com/api/docs/models/gpt-5.2)
---

Repository: [GitHub](https://github.com/SomeNewKid/BankSupportOfficer)

::: /SIDEBAR :::
