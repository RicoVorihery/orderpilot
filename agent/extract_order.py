from dotenv import load_dotenv
import anthropic
from anthropic.types import MessageParam

from models import ExtractedOrder

load_dotenv()
client = anthropic.Anthropic()


def extract_order_simple(email_sender: str, email_object: str, email_text: str):
    messages: list[MessageParam] = [
        {
            "role": "user",
            "content": f"""Extract the key information from this email: 
            {email_sender} {email_object} {email_text}""",
        }
    ]

    response = client.messages.parse(
        model="claude-opus-4-5",
        max_tokens=1024,
        messages=messages,
        output_format=ExtractedOrder,
    )

    extractedOrder = response.parsed_output
    print(extractedOrder)


def create_first_message(
    sender: str, email_object: str, email_text: str
) -> MessageParam:
    content = f"""Voici l'email à traiter :
De : {sender}
Objet : {email_object}
Message : {email_text}"""
    return {"role": "user", "content": content}


def extract_order_chat(conversation: list[MessageParam], system_prompt):
    while True:
        # 1. First call
        response = client.messages.create(
            model="claude-opus-4-5",
            max_tokens=1024,
            system=system_prompt,
            messages=conversation,
        )

        assistant_reply = (
            next(block for block in response.content if block.type == "text")
        ).text

        if "READY_TO_EXTRACT" in assistant_reply:
            conversation.append({
                "role": "user",
                "content": "Parfait, procède à l'extraction.",
            })
            break

        conversation.append({"role": "assistant", "content": assistant_reply})
        print(assistant_reply)
        email_response = input("Réponse:")
        conversation.append({"role": "user", "content": email_response})

    response = client.messages.parse(
        model="claude-opus-4-5",
        system=system_prompt,
        max_tokens=1024,
        messages=conversation,
        output_format=ExtractedOrder,
    )

    return response.parsed_output
