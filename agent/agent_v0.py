from dotenv import load_dotenv
import anthropic
from anthropic.types import MessageParam
from extract_order import create_first_message, extract_order_chat
import json
from pathlib import Path

load_dotenv()
client = anthropic.Anthropic()

parent_folder = Path(__file__).parent.parent
emails_json = parent_folder / "tests" / "emails.json"

with open(emails_json, "r", encoding="utf-8") as f:
    emails = json.load(f)

system_prompt = """Tu es un assistant qui extrait des commandes depuis des emails. 
Chaque commande devrait y avoir un libellé de l'article, la référence du produit et la quantité à commander.
Si des infos manquent, liste TOUTES les informations manquantes dans un seul message.
Si un article ne semble pas être une pièce automobile, demande une confirmation au client avant de l'inclure dans la commande. 
Tes messages doivent être formulées comme un email professionnel adressé au client, avec une salutation et une signature 'L'équipe des commandes'.
Quand tu as tout: réponds-moi EXACTEMENT et UNIQUEMENT par un signal de confirmation "READY_TO_EXTRACT"."""

for email in emails:
    conversation = [
        create_first_message(email["sender"], email["object"], email["text"])
    ]
    order = extract_order_chat(conversation, system_prompt)
    print(f"\n--- {email['id']} ---")
    print(order)
