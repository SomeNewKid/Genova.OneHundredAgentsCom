# Leave Request Checker

Leave Request Checker is a small proof of concept for using LangChain to shape an artificial intelligence (AI) workflow. It lets an employee type a request in ordinary language, such as asking for leave on the last Friday of next month, and turns that into a structured leave request which is then verified.

The workflow uses the model where language is messy: interpreting the employee’s request. Once the requested day is identified, the rest of the process behaves more like a normal business rule check. It verifies whether the employee appears eligible and whether enough notice has been provided, then reports the result in plain language.

This sketches a realistic pattern for business software. Some steps benefit from AI because people do not speak in database fields. Other steps should stay deterministic because policy checks need to be inspectable. LangChain helps connect those parts without pretending the whole system needs to be magic.

::: SIDEBAR :::

Language: Python
Framework: [LangChain](https://www.langchain.com/)
Pattern: Non-agent workflow
Model: [GPT-4o](https://developers.openai.com/api/docs/models/gpt-4o)
---

Repository: [GitHub](https://github.com/SomeNewKid/LeaveRequestChecker)

::: /SIDEBAR :::

