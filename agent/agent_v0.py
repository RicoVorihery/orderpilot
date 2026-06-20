from dotenv import load_dotenv
import anthropic
from extract_order import create_first_message, extract_order_chat
import json
from pathlib import Path
from prompts import EXTRACT_ORDER_SYSTEM_PROMPT

load_dotenv()
client = anthropic.Anthropic()

parent_folder = Path(__file__).parent.parent
emails_json = parent_folder / "tests" / "emails.json"

with open(emails_json, "r", encoding="utf-8") as f:
    emails = json.load(f)


for email in emails:
    conversation = [
        create_first_message(email["sender"], email["object"], email["text"])
    ]
    order = extract_order_chat(conversation, EXTRACT_ORDER_SYSTEM_PROMPT)
    print(f"\n--- {email['id']} ---")
    print(order)
