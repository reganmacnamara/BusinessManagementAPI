# BusinessManagementAPI

A RESTful Web API for managing clients, products, invoices, and receipts — built with ASP.NET Core 8 and Entity Framework Core.

The core workflow: clients receive **invoices** for products/services, and **receipts** are recorded as payments come in. Receipt items are allocated to specific invoices, automatically tracking outstanding balances.

---

## Features

- Full CRUD for **Clients**, **Products**, **Invoices**, and **Receipts**
- **Invoice & Receipt line items** with upsert support (create or update in one endpoint)
- **Payment allocation** — receipt items are linked to invoices, with automatic outstanding balance tracking
- **PDF generation** for invoices and receipts via QuestPDF
- **Swagger UI** for interactive API exploration
- EF Core **code-first migrations** with SQL Server

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8.0 |
| ORM | Entity Framework Core 8.0 |
| Database | SQL Server |
| Mapping | AutoMapper 16 |
| PDF | QuestPDF 2024.10 |
| API Docs | Swashbuckle / Swagger |

---

## Architecture

The project follows a **Request / Handler / Response** pattern (CQRS-like):

```
Controller → Handler (UseCase) → BaseHandler → EF Core (SQLContext)
```

```
BusinessManagementAPI/
├── Controllers/         # Route definitions — delegate to use case handlers
├── UseCases/            # One folder per domain (Clients, Invoices, Products, Receipts)
│   └── <Domain>/
│       └── <UseCase>/   # Request, Handler, Response files
├── Entities/            # Domain models (Client, Product, Invoice, Receipt, ...)
├── Data/                # SQLContext + EF Core entity configurations
├── Services/            # AllocationService, PdfService
└── Profiles/            # AutoMapper mapping profiles
```

---

## API Endpoints (WIP)

### Clients
| Method | Route | Description |
|---|---|---|
| `GET` | `/Client` | List all clients |
| `GET` | `/Client/{id}` | Get a client |
| `POST` | `/Client` | Create a client |
| `PATCH` | `/Client` | Update a client |
| `DELETE` | `/Client/{id}` | Delete a client |

### Products
| Method | Route | Description |
|---|---|---|
| `GET` | `/Product` | List all products |
| `GET` | `/Product/{id}` | Get a product |
| `POST` | `/Product` | Create a product |
| `PATCH` | `/Product` | Update a product |
| `DELETE` | `/Product/{id}` | Delete a product |

### Invoices
| Method | Route | Description |
|---|---|---|
| `GET` | `/Invoice` | List all invoices |
| `GET` | `/Invoice/{id}` | Get an invoice |
| `GET` | `/Invoice/Client/{clientId}` | Get all invoices for a client |
| `GET` | `/Invoice/{id}/pdf` | Download invoice as PDF |
| `POST` | `/Invoice` | Create an invoice |
| `PATCH` | `/Invoice` | Update an invoice |
| `PUT` | `/Invoice/Item` | Create or update an invoice line item |
| `DELETE` | `/Invoice/{id}` | Delete an invoice |
| `DELETE` | `/Invoice/Item/{id}` | Delete an invoice line item |

### Receipts
| Method | Route | Description |
|---|---|---|
| `GET` | `/Receipt` | List all receipts |
| `GET` | `/Receipt/{id}` | Get a receipt |
| `GET` | `/Receipt/Client/{clientId}` | Get all receipts for a client |
| `GET` | `/Receipt/{id}/pdf` | Download receipt as PDF |
| `POST` | `/Receipt` | Create a receipt |
| `PATCH` | `/Receipt` | Update a receipt |
| `PUT` | `/Receipt/Item` | Create or update a receipt line item (allocates to invoice) |
| `DELETE` | `/Receipt/{id}` | Delete a receipt |
| `DELETE` | `/Receipt/Item/{id}` | Delete a receipt line item |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)

### Setup

1. Clone the repository:

```bash
git clone https://github.com/reganmacnamara/BusinessManagementAPI.git
cd BusinessManagementAPI
```

2. Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=InvoiceAutomationLocal;Trusted_Connection=True;"
}
```

3. Apply migrations:

```bash
dotnet ef database update
```

4. Run the API:

```bash
dotnet run
```

Swagger UI will be available at `https://localhost:{port}/swagger`.
