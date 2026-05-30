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

email_object2 = "Besoin de pièces pour demain matin"
email_text2 = """Bonjour,

C'est Mario du garage à Sainte-Marie. J'ai un char en shop, 
un Dodge Ram 2015, j'ai besoin de la pompe à eau pis les 
courroies qui vont avec. Vous devriez avoir ça en stock.

Rapplez-moi si vous avez des questions.

Mario Gagnon
Auto Mario Gagnon
418-555-0147"""

email_object3 = "Commande pièces Ford"
email_text3 = """Bonjour,

Je voudrais commander des plaquettes de frein arrière 
pour Ford Escape 2021 (réf. BP-9921) et des filtres à 
air (réf. FA-3301). 

Livraison à Lévis svp.

Sophie Bouchard
Garage Bouchard"""

messages: list[MessageParam] = [
    {
        "role": "user",
        "content": f"Extract the key information from this email: {sender} {email_object3} {email_text3}",
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
