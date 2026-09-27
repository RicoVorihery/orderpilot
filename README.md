# OrderPilot

> Integrating AI agents into .NET enterprise systems, without rewriting everything.

B2B order emails arrive in natural language. Clients write in English or French, abbreviate product names, forget quantities. OrderPilot is an open-source AI agent that reads these emails, asks clarifications when needed, and creates structured draft orders in a .NET ERP, automatically.

**The real problem it solves:** 

95% of enterprise AI pilots fail at integration, not at the model.
OrderPilot demonstrates the full bridge: a Python AI agent (LangGraph + Claude) communicating 
with a production-grade .NET 10 API via MCP, the pattern most AI engineers can't build because 
they don't know .NET.

## Status
🚧 Work in progress, public build over 6 months (May–November 2026)

Week 1-2 complete: Claude API + tool calling + structured extraction + .NET API with EF Core + PostgreSQL

## Architecture

Email → Python Agent (Claude + LangGraph) → MCP Server (C#) → StockDemo.Api (.NET 10) → PostgreSQL

> Pattern: Strangler Fig, the AI layer wraps existing .NET systems without touching legacy code.

## Tech Stack

| Agent Side | API Side |
|------------|----------|
| Python 3.12 | .NET 10 / ASP.NET Core |
| Anthropic Claude API | Entity Framework Core |
| LangGraph (Month 2) | PostgreSQL |
| MCP Client | Docker |
| Pydantic v2 | Scalar (OpenAPI) |

## Getting Started

Two components to run locally:

- **Python Agent** → see [Agent Setup](docs/setup.md)
- **.NET API** → see [StockDemo.Api Setup](StockDemo.Api/README.md)

## Roadmap

| Month | Focus | Status |
|-------|-------|--------|
| 1 | LLM fundamentals + StockDemo .NET API | ✅ Complete |
| 2 | LangGraph + C# MCP Server | 🔜 Next |
| 3 | RAG + pgvector + eval framework | ⏳ Planned |
| 4 | Observability (Langfuse) + Azure deployment | ⏳ Planned |
| 5 | Public launch + documentation | ⏳ Planned |

## License

MIT
