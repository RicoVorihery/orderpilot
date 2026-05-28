from dotenv import load_dotenv
from datetime import datetime
from zoneinfo import ZoneInfo
import json
import anthropic
from anthropic.types import ToolParam, MessageParam, ToolUseBlock

load_dotenv()

client = anthropic.Anthropic()


def get_weather(city: str):
    return "Montréal : 22°C, ensoleillé"


def get_time(timezone: str):
    tz = ZoneInfo(timezone)
    return datetime.now(tz).strftime("%A %Y-%m-%d %H:%M:%S")


def add_numbers(a: float, b: float):
    return a + b


TOOLS_MAP = {
    "get_weather": get_weather,
    "get_time": get_time,
    "add_numbers": add_numbers,
}


def run_tool(name, tool_input):
    fn = TOOLS_MAP.get(name)
    if fn is None:
        raise ValueError(f"Unknown tool: {name}")
    return fn(**tool_input)


tools: list[ToolParam] = [
    {
        "name": "get_weather",
        "description": "Get the current weather in a given city.",
        "input_schema": {
            "type": "object",
            "properties": {
                "city": {"type": "string", "description": "The name of the city"}
            },
            "required": ["city"],
        },
    },
    {
        "name": "get_time",
        "description": "Get the current day, date and time for a given timezone.",
        "input_schema": {
            "type": "object",
            "properties": {
                "timezone": {
                    "type": "string",
                    "description": "The timezone name (e.g. America/Toronto).",
                }
            },
            "required": ["timezone"],
        },
    },
    {
        "name": "add_numbers",
        "description": "Return the sum of two numbers.",
        "input_schema": {
            "type": "object",
            "properties": {
                "a": {"type": "number", "description": "The first number"},
                "b": {"type": "number", "description": "The second number"},
            },
            "required": ["a", "b"],
        },
    },
]


messages: list[MessageParam] = [
    {
        "role": "user",
        "content": "Quel temps fait-il à Montréal, quelle heure est-il à America/Toronto, et combien font 42 + 58 ?",
    }
]

response = client.messages.create(
    model="claude-opus-4-5", max_tokens=1024, tools=tools, messages=messages
)

tool_results = []
tool_uses: list[ToolUseBlock] = []
while response.stop_reason == "tool_use":
    tool_uses = [block for block in response.content if block.type == "tool_use"]

    for tool_use in tool_uses:
        result = run_tool(tool_use.name, tool_use.input)
        tool_results.append({
            "type": "tool_result",
            "tool_use_id": tool_use.id,
            "content": json.dumps(result),
        })

    messages.append({"role": "assistant", "content": response.content})
    messages.append({"role": "user", "content": tool_results})

    response = client.messages.create(
        model="claude-opus-4-5", max_tokens=1024, tools=tools, messages=messages
    )

final_text = next(block for block in response.content if block.type == "text")
print(final_text.text)
