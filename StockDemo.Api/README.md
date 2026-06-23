# StockDemo.Api

Fake .NET 10 ERP API simulating a Quebec auto parts distributor. Used as the backend for the OrderPilot AI agent.

## Prerequisites

- .NET 10 SDK
- Docker Desktop

## Getting Started

1. Start the database

```bash
docker compose up -d
```

2. Run the API

```bash
cd StockDemo.Api
dotnet run
```

3. Open the API explorer

http://localhost:5007/scalar/v1

## Authentication

All endpoints require an API key header:

```bash
x-api-key: your_api_key
```

Configure your key in `appsettings.Development.json`:

```json
"ApiSettings": {
    "ApiKey": "your_api_key_here"
}
```

## Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| GET | /api/products | List products (supports ?search=) |
| GET | /api/products/{id} | Get product by id |
| GET | /api/customers/by-email/{email} | Get customer by email |
| GET | /api/stocks/{productId} | Get total stock for a product |
| POST | /api/orders/drafts | Create a draft order |
| GET | /api/orders/{id} | Get order by id |

## Seed Data

Automatically seeded on first run in Development: 50 products, 10 customers, 2 warehouses, ~100 stock entries.