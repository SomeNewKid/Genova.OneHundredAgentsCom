# Maths helper

**Maths helper** is a small proof of concept for testing how far AWS Strands Agents and Bedrock AgentCore can take a practical calculation task. The user asks a plain-language question, such as the total cost of six books after a discount, and the agent works out the answer without asking the user to translate the problem into a formula. That is a modest task, but a useful one: many business questions start as messy words before they become tidy numbers.

The interesting part is not the arithmetic. A spreadsheet could handle that while half asleep. The point is that the agent uses AgentCore’s built-in Code Interpreter tool, rather than a custom calculator function, to run the computation in a managed sandbox. That points toward safer uses such as checking invoices, validating pricing scenarios, or analysing small datasets without giving the model free rein to improvise maths.

This project also tests the developer experience around Strands Agents and AgentCore deployment. After the usual first-use wrestling match, the pattern is fairly approachable: build the agent locally, run it through AgentCore, and deploy it to AWS when needed.

::: SIDEBAR :::

Language: Markdown
Framework: [Strands Agent](https://strandsagents.com/)
Platform: [AWS Bedrock AgentCore](https://aws.amazon.com/bedrock/agentcore/)
Pattern: Single agent
Model: [Claude Sonnet 4.6](https://www.anthropic.com/news/claude-sonnet-4-6)
---

Repository: [GitHub](https://github.com/SomeNewKid/NutshellSummarizer)

::: /SIDEBAR :::
