# Model recommender

**Model Recommender** explores a practical first step with Anthropic’s Claude Agent SDK: giving an agent useful abilities without handing it the keys to the shed. The user describes the kind of local AI program they want to run, such as an agent that helps write Python applications. The agent checks the machine profile it is given, researches current local models, and recommends options that are likely to fit the available hardware.

The business idea is simple but useful. Choosing local models is awkward because capability, memory use, context window, and runtime support all matter. This agent sketches a lightweight advisor that can turn a user’s goal and laptop constraints into a short, practical shortlist. It can also explain why some models are a poor fit, which is often more valuable than another optimistic download link.

The main lesson is about guardrails. The Claude Agent SDK lets the project allow web research while denying shell and file tools. That makes the experiment less about “can an agent answer?” and more about “can it answer inside clear boundaries?” For a small agent, that is exactly the interesting bit.

::: SIDEBAR :::

Language: Python
Framework: [Claude Agent SDK](https://claude.com/blog/building-agents-with-the-claude-agent-sdk)
Pattern: Single agent
Models: [Claude Opus 4.8](https://www.anthropic.com/claude/opus) | [Claude Haiku 4.5](https://www.anthropic.com/claude/haiku)
---

Repository: [GitHub](https://github.com/SomeNewKid/ModelRecommender)

::: /SIDEBAR :::
