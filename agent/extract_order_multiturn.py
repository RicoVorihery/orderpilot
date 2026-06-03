from dotenv import load_dotenv
import anthropic
from anthropic.types import MessageParam
from models import ExtractedOrder

load_dotenv()
client = anthropic.Anthropic()

system_prompt = """Tu es un assistant qui extrait des commandes depuis des emails. 
Chaque commande devrait y avoir un libellé de l'article, la référence du produit et la quantité à commander.
Si des infos manquent, liste TOUTES les informations manquantes dans un seul message. 
Tes messages doivent être formulées comme un email professionnel adressé au client, avec une salutation et une signature 'L'équipe des commandes'.
Quand tu as tout: réponds-moi EXACTEMENT et UNIQUEMENT par un signal de confirmation "READY_TO_EXTRACT"."""


def create_first_message(
    sender: str, email_object: str, email_text: str
) -> MessageParam:
    content = f"""Voici l'email à traiter :
De : {sender}
Objet : {email_object}
Message : {email_text}"""
    return {"role": "user", "content": content}


sender = "jean.tremblay@garagepro.qc.ca"
email_object2 = "Besoin de pièces pour demain matin"
email_text2 = """Bonjour,

C'est Mario du garage à Sainte-Marie. J'ai un char en shop, 
un Dodge Ram 2015, j'ai besoin de la pompe à eau pis les 
courroies qui vont avec. Vous devriez avoir ça en stock.

Rapplez-moi si vous avez des questions.

Mario Gagnon
Auto Mario Gagnon
418-555-0147"""


def extract_order_chat(conversation: list[MessageParam]):
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


conversation: list[MessageParam] = []
conversation.append(create_first_message(sender, email_object2, email_text2))

extractedOrder = extract_order_chat(conversation)
print(extractedOrder)
