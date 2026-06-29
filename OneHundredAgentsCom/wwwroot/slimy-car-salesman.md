# Slimy Car Salesman

Slimy Car Salesman explores guardrails in Microsoft’s Agent Framework through a deliberately slippery car sales scenario. The user asks for the price of a car, the agent looks up the answer, and then a chain of checks tries to stop the true price from reaching the customer. It is not a serious sales assistant, unless your sales strategy is “be evasive with impressive consistency.” It is a small test of where control can be added around an agent workflow.

The useful part is not the car lot. It is the six places where intervention is possible: before and after the agent runs, before and after the model is called, and before and after a tool is used. Each guardrail can inspect, block, or alter what is happening at that stage. That makes the sample a compact way to see how business rules, safety checks, and policy controls might fit around an AI agent.

The takeaway is practical. Framework guardrails are useful and visible, but they are only part of the safety story. Stronger systems still need infrastructure controls, sandboxes, permissions, and boring old engineering discipline.

::: SIDEBAR :::

Language: C#
Framework: [Agent Framework](https://learn.microsoft.com/agent-framework/)
Pattern: Single agent
Model: [GPT-4.1-mini](https://developers.openai.com/api/docs/models/gpt-4.1-mini)
---

Repository: [GitHub](https://github.com/SomeNewKid/SlimyCarSalesman)

::: /SIDEBAR :::
