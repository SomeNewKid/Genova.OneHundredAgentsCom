# White Belt Hacker

White Belt Hacker is a small experiment with the Claude Agent SDK, built to see how much control a developer can place around an agent that is trying to complete a goal. The scenario is deliberately plain: the agent is asked to find information it should not be able to access. That modest setup keeps the interesting part visible: how the agent changes tactics when its obvious tools are taken away.

The useful business lesson is not about protecting one text file. It is about capability management. By default, an agent can be surprisingly resourceful. If it cannot use one route, it may look for another: search tools, helper agents, workflow tools, or other indirect paths. The SDK makes those attempts observable and configurable, so a team can start turning broad autonomy into a narrower, more intentional operating space.

This project sketches a practical governance pattern for agent work. Use SDK controls such as allowed and disallowed tools, log what the agent tries, and expect to learn something mildly uncomfortable. Those controls matter, but they sit below stronger boundaries such as sandboxing and infrastructure-level isolation. In other words: a useful seatbelt, not a bank vault.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Models: [Claude Opus 4.8](https://www.anthropic.com/claude/opus) | [Claude Haiku 4.5](https://www.anthropic.com/claude/haiku)
---

Repository: [GitHub](https://github.com/SomeNewKid/WhiteBeltHacker)

::: /SIDEBAR :::
