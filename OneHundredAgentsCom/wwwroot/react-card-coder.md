# React Card Coder

React Card Coder was built to test a simple but important question: can a coding agent check its work by looking at the webpage the way a user would? Not by reading source code and hoping for the best, but by opening the page, moving the mouse, taking screenshots, and using those images as evidence. That is the interesting bit here. The card flip is just the test subject; the real experiment is visual feedback inside the agent loop.

The agent uses Anthropic’s Claude Agent SDK to work with a constrained local webpage. It can capture the normal state, hover over the card, capture the changed state, and ask the model to evaluate whether the result matches the requirement. When the page does not behave correctly, the agent can update the React script and try again. This sketches a practical business pattern: given a visual requirement for a webpage or component, an agent can make a change, interact with the UI, and compare the result against the intended behaviour.

This is not a finished web QA system. It is a proof of concept with a very small target. But it shows something real: agents do not have to stay trapped in text. They can poke the interface, look at what happened, and use that feedback to improve their own work.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Models: [Claude Opus 4.8](https://www.anthropic.com/claude/opus) | [Claude Haiku 4.5](https://www.anthropic.com/claude/haiku)
---

Repository: [GitHub](https://github.com/SomeNewKid/ReactCardCoder)

::: /SIDEBAR :::
