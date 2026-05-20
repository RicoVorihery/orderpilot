from dotenv import load_dotenv
import anthropic

load_dotenv()

client = anthropic.Anthropic()
message = client.messages.create(
    model="claude-opus-4-5",
    max_tokens=1024,
    messages=[{"role": "user", "content": "Dis bonjour à Patricko"}],
)

text = next((block.text for block in message.content if block.type == "text"), None)
print(text)
