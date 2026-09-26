import requests
import os
from typing import Optional
from clients.models import (
    ProductResponse,
    CustomerResponse,
    OrderRequest,
    OrderResponse,
)
from pydantic import ValidationError


class StockDemoClient:
    def __init__(self):
        self.base_url = os.getenv("STOCKDEMO_API_URL", "http://localhost:5007")
        self.api_key = os.getenv("STOCKDEMO_API_KEY", "")

    def search_products(self, search: str) -> list[ProductResponse]:
        url = f"{self.base_url}/api/products"
        params = {"search": search}
        headers = {"x-api-key": self.api_key}

        try:
            response = requests.get(url, params=params, headers=headers, timeout=10)
            response.raise_for_status()

            raw_products = response.json()

            return [ProductResponse(**prod) for prod in raw_products]
        except requests.exceptions.RequestException as e:
            print(f"HTTP request error:{e}")
            return []

        except ValidationError as e:
            print(f"Pydantic validation error:{e}")
            return []

    def get_customer_by_email(self, email: str) -> Optional[CustomerResponse]:
        url = f"{self.base_url}/api/customers/by-email/{email}"
        headers = {"x-api-key": self.api_key}

        try:
            response = requests.get(url, headers=headers, timeout=10)
            response.raise_for_status()

            raw_customer = response.json()

            return CustomerResponse(**raw_customer)

        except requests.exceptions.RequestException as e:
            print(f"HTTP request error:{e}")
            return None

        except ValidationError as e:
            print(f"Pydantic validation error:{e}")
            return None

    def check_stock(self, product_id: int) -> int:
        url = f"{self.base_url}/api/stocks/{product_id}"
        headers = {"x-api-key": self.api_key}

        try:
            response = requests.get(url, headers=headers, timeout=10)
            response.raise_for_status()

            return response.json()

        except requests.exceptions.RequestException as e:
            print(f"HTTP request error:{e}")
            return 0

    def create_draft_order(self, order: OrderRequest) -> Optional[OrderResponse]:
        url = f"{self.base_url}/api/orders/drafts"
        headers = {"x-api-key": self.api_key, "Content-type": "application/json"}

        try:
            response = requests.post(
                url,
                json=order.model_dump(by_alias=True, mode="json"),
                headers=headers,
                timeout=10,
            )
            response.raise_for_status()

            data = response.json()
            return OrderResponse(**data)

        except requests.exceptions.RequestException as e:
            print(f"HTTP request error:{e}")
            return None

        except ValidationError as e:
            print(f"Pydantic validation error:{e}")
            return None
