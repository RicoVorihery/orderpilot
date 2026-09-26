from pydantic import BaseModel, ConfigDict
from pydantic.alias_generators import to_camel
from typing import Optional
from datetime import datetime


class BaseModelWithConfig(BaseModel):
    model_config = ConfigDict(alias_generator=to_camel, populate_by_name=True)


class ProductResponse(BaseModelWithConfig):
    id: int
    reference: str
    label: str
    brand: str
    description: Optional[str]
    price: float


class CustomerResponse(BaseModelWithConfig):
    id: int
    lastname: str
    firstname: str
    company_name: str
    address: str
    city: str
    province: str
    postal_code: str
    email: str
    phone: str


class StockResponse(BaseModelWithConfig):
    id: int
    product_id: int
    warehouse_id: int
    quantity: int


class OrderItemRequest(BaseModelWithConfig):
    product_id: int
    quantity: int
    unit_price: float


class OrderRequest(BaseModelWithConfig):
    customer_id: int
    date_order: datetime
    order_lines: list[OrderItemRequest]


class OrderItemResponse(BaseModelWithConfig):
    id: int
    order_id: int
    product_id: int
    quantity: int
    unit_price: float


class OrderResponse(BaseModelWithConfig):
    id: int
    customer_id: int
    status: str
    date_order: datetime
    order_lines: list[OrderItemResponse]
