# Refactoring Coder

Refactoring Coder is a small proof of concept built to take first steps with Anthropic's Agent SDK. The idea is simple enough to keep the moving parts visible: point an agent at an existing bit of code, ask it to work out the current behaviour, write unit tests for that behaviour, and only then refactor the code. That is the interesting part. The agent is not just chatting about code. It is using local tools to read files, save changes, and run the project checks.

The business shape is familiar. Many teams have old code that works, mostly, but is hard to improve because nobody wants to be the person who breaks it. Refactoring Coder sketches a safer workflow: first capture what the code already does, then clean it up with tests watching over the result. It is not a full coding assistant, and it is not pretending to be one. Sensible restraint is doing some work here.

What this experiment shows is that Anthropic's SDK can connect a Claude model to local project actions without much ceremony. That makes small, task-focused agents feel practical rather than mythical.

::: SIDEBAR :::

Language: Python
Framework: [Anthropic Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Model: [Claude Sonnet 4.5](https://www.anthropic.com/news/claude-sonnet-4-5)
---

Repository: [GitHub](https://github.com/SomeNewKid/RefactoringCoder)

::: /SIDEBAR :::
