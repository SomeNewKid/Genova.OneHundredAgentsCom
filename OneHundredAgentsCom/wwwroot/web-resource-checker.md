# Web resource checker

**Web resource checker** is a small proof of concept for trying specialist Skills with Anthropic’s Claude Agent SDK. It takes a URL, fetches the resource, checks the resource type, and asks Claude to use the matching Skill. HTML, CSS, JavaScript, and image resources each get their own checklist.

The interesting part is not the checklist itself. The checklists are intentionally simple. The useful idea is that Skills package instructions in a form the Claude Agent SDK knows how to discover and apply. Instead of writing one giant prompt that tries to cover every possible resource, the application gives Claude a focused skill only when that skill fits the fetched content.

That sketches a practical business pattern. A company could route different documents, assets, or records to different review playbooks without making every workflow live in one sprawling instruction soup. This version checks web resources, modestly and a bit mechanically, but it shows the shape of agents that pick the right specialist guidance at the right moment.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Models: [Claude Opus 4.8](https://www.anthropic.com/claude/opus) | [Claude Haiku 4.5](https://www.anthropic.com/claude/haiku)
---

Repository: [GitHub](https://github.com/SomeNewKid/WebResourceChecker)

::: /SIDEBAR :::
