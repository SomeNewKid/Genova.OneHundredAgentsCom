# Security headers auditor

**Security headers auditor** was built to test a very specific question: can Codex CLI turn a plain requirements document into a working Python console application, while also following local project rules? The business task is deliberately simple. Given a website, the generated tool crawls internal pages and checks whether each page returns a small set of expected security headers. Pages that pass are shown in green; pages missing headers are shown in red with the missing names listed.

The crawler is not the star of the exercise. It is the measuring stick. The interesting part is that Codex CLI read the requirements, worked inside the existing project structure, produced the application, and ran the project check script covering formatting, linting, type checking, and tests. That is a useful shape for business work: write down the outcome, give the agent local rules, and ask it to build against them.

This remains a proof of concept, not a claim that every larger requirement will behave nicely. Small tasks are where agents often look their best. Still, this one shows that Codex CLI can produce a complete, checked application from a requirements file, which is a result worth paying attention to.

::: SIDEBAR :::

Language: Python
Agent: [Codex CLI](https://developers.openai.com/codex/cli)
Pattern: Coding agent
Model: [GPT-5.5](https://developers.openai.com/api/docs/models/gpt-5.5)
---
Repository: [GitHub](https://github.com/SomeNewKid/SecurityHeadersAuditor)

::: /SIDEBAR :::
