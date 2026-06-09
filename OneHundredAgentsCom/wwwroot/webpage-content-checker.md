# Webpage Content Checker

Webpage Content Checker was built less as a website auditor and more as a test bench for delegating coding work to Google’s Jules. The business sketch is simple: give the tool a website, let it walk through the pages it can find, and report whether each page passes a small set of content-quality checks.

The interesting part is the shape of the work left open. The checker starts with only a few basic rules, such as whether a page has a usable title and main heading. New rules are meant to be added one at a time, each as a small, testable unit. That makes it a tidy proving ground for asking Jules to add useful behaviour without handing it the whole steering wheel and hoping for the best.

This matters because Jules is not trying to be another chatty coding partner like Codex or Claude Code. It is aimed at autonomous, asynchronous work in the cloud. This project tests what that handoff might feel like in a real business task: small, bounded, reviewable changes.

::: SIDEBAR :::

Language: Python
Agent: [Jules by Google](https://jules.google/)
Pattern: Coding agent
Models: [GPT-5.5](https://developers.openai.com/api/docs/models/gpt-5.5) | [Gemini 2.5](https://ai.google.dev/gemini-api/docs/models/gemini-2.5-pro)
---

Repository: [GitHub](https://github.com/SomeNewKid/WebpageContentChecker)

::: /SIDEBAR :::

