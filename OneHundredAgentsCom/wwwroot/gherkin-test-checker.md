# Gherkin test checker

**Gherkin test checker** was built to take first steps with non-agent workflows in BeeAI. It sketches a familiar business task: a team has written a Gherkin feature for behaviour-driven or test-driven development, and wants a quick check that the document has the expected shape before it moves further through a delivery process.

The checker reads a local feature file, turns it into a small internal representation, runs validation, and prints a plain result. It checks for things such as a named feature, scenarios, and the basic Given, When, Then structure. The point is not to crown a new king of Gherkin validation. It is to see whether BeeAI can keep a workflow readable when the work is closer to ordinary business logic than chatty agent behaviour.

What I like about this proof of concept is that BeeAI does not feel wildly distant from plain Python. The workflow steps still look practical: read, parse, validate, summarize. But putting them into BeeAI gives the process a shape that could later grow into something more agentic, such as explaining failures, suggesting fixes, or routing uncertain cases for review.

::: SIDEBAR :::

Language: Python
Framework: [IBM BeeAI Framework](https://framework.beeai.dev)
Pattern: Non-agent workflow
---

Repository: [GitHub](https://github.com/SomeNewKid/GherkinTestChecker)

::: /SIDEBAR :::
