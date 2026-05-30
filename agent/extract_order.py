from dotenv import load_dotenv
import anthropic
from anthropic.types import MessageParam

from models import ExtractedOrder

load_dotenv()
client = anthropic.Anthropic()

sender = "jean.tremblay@garagepro.qc.ca"
email_object = "Commande urgente - pièces Civic et F-150"
email_text = """Bonjour,
Je suis Jean Tremblay du Garage Pro à Drummondville.

J'aurais besoin rapidement des pièces suivantes :

- 4 plaquettes de frein avant pour Honda Civic 2019 (réf. BP-4421)
- 2 filtres à huile pour Ford F-150 2020 (réf. FO-8834)
- 1 courroie de distribution Toyota Camry 2018 (réf. CT-1156)

Pouvez-vous confirmer la disponibilité et le délai de livraison ?

Merci,
Jean Tremblay
Garage Pro Drummondville
450-555-0192"""

messages: list[MessageParam] = [
    {
        "role": "user",
        "content": "Extract the key information from this email:\n"
        + sender
        + "\n"
        + email_object
        + "\n"
        + email_text,
    }
]

response = client.messages.parse(
    model="claude-opus-4-5",
    max_tokens=1024,
    messages=messages,
    output_format=ExtractedOrder,
)

print(response.parsed_output)
