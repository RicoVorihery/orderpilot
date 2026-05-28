from pydantic import BaseModel, Field
from typing import Optional, List
from datetime import datetime

# OrderItem(label, product_reference, quantity, #extracted_order)
# Customer(firstname, lastname, company, #customer_id)
# ExtracterdOrder(email_object, email_text, date, #customer_id, orderItems)


class OrderItem(BaseModel):
    label: str = Field(..., description="The product label as mentioned in the email")
    product_reference: str = Field(..., description="Reference of the product")
    quantity: int = Field(..., description="Quantity of items ordered")


class Customer(BaseModel):
    firstname: str = Field(..., description="Firstname of the customer")
    lastname: str = Field(..., description="Lastname of the customer")
    company: str = Field(..., description="The company name of the customer")
    customer_id: Optional[int] = Field(
        None, description="The customer id from the database"
    )


class ExtractedOrder(BaseModel):
    email_object: str = Field(
        ..., description="The email object of the extracted order"
    )
    email_text: str = Field(..., description="The email content")
    date: datetime = Field(..., description="The date of the email")
    customer_id: Optional[int] = Field(
        None, description="The customer id from the database"
    )
    order_items: List[OrderItem] = Field(..., description="List of the order items")
