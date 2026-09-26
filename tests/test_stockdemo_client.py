import os
import sys

ROOT_DIR = os.path.abspath(os.path.join(os.path.dirname(__file__), ".."))
if ROOT_DIR not in sys.path:
    sys.path.insert(0, ROOT_DIR)

from dotenv import load_dotenv  # noqa: E402
from clients.stockdemo_client import StockDemoClient  # noqa: E402
from clients.models import (  # noqa: E402
    OrderRequest,
    OrderItemRequest,
)
from datetime import datetime, timezone  # noqa: E402

load_dotenv()
client = StockDemoClient()


def test_search_products():
    products = client.search_products("filtre")
    print(products)


def test_get_customer_by_email(email: str):
    customer = client.get_customer_by_email(email)
    print(customer)


def test_create_draft_order():
    order = OrderRequest(
        customer_id=7,
        date_order=datetime.now(timezone.utc),
        order_lines=[OrderItemRequest(product_id=8, quantity=1, unit_price=235.99)],
    )

    result = client.create_draft_order(order)
    print(result)


if __name__ == "__main__":
    # test_search_products()
    email = "mario.gagnon@gagnongarage.ca"
    # test_get_customer_by_email(email)
    test_create_draft_order()
