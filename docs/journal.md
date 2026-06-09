# Learning Journal

## Week 1 — LLM Fundamentals

### What I learned
- How to call the Claude API and handle response content blocks (TextBlock, ToolUseBlock)
- Tool calling mechanics: defining tools, handling the agentic loop, parallel tool execution
- Structured outputs with `messages.parse()` and Pydantic models — cleaner than the tool calling trick
- Building a multi-turn clarification loop with a deterministic signal (`READY_TO_EXTRACT`)
- Processing a batch of emails through a single agent entry point

### What was difficult
- Tool calling took time to internalize,vthe fact that Claude doesn't execute anything itself, 
  it only *requests* execution, was the key mental shift
- Structured outputs were a pleasant surprise, much simpler than expected once the 
  `output_config` / `messages.parse()` API clicked

### What I would do differently
- Plan the file and service structure upfront before coding, I refactored `extract_order.py` mid-week when I realized the multi-turn and simple extraction should live in the same module

### Week 2 goal
Build StockDemo .NET API running locally with Docker, accessible via Swagger, with minimal seed data.