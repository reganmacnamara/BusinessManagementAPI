<p align="center">
  <h1 align="center">MacsBusinessManagementAPI</h1>
  <p align="center">
    A clean, extensible RESTful Web API for managing clients, products, invoices, and receipts — built with ASP.NET Core 8 and Entity Framework Core.
  </p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/EF_Core-8.0-512BD4?logo=dotnet&logoColor=white" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black" alt="Swagger" />
  <img src="https://img.shields.io/badge/License-MIT-blue" alt="License" />
  <img src="https://img.shields.io/badge/C%23-100%25-239120?logo=csharp&logoColor=white" alt="C#" />
</p>

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running the API](#running-the-api)
- [Authentication](#authentication)
- [API Reference](#api-reference)
  - [Clients](#clients)
  - [Products](#products)
  - [Invoices](#invoices)
  - [Receipts](#receipts)
- [Core Concepts](#core-concepts)
  - [Invoice & Receipt Workflow](#invoice--receipt-workflow)
  - [Payment Allocation](#payment-allocation)
  - [PDF Generation](#pdf-generation)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [Roadmap](#roadmap)
- [License](#license)

---

## Overview

**MacsBusinessManagementAPI** is a backend API designed for small-to-medium businesses that need to track clients, catalog products and services, issue invoices, and record incoming payments via receipts. It follows a straightforward real-world workflow:

1. **Clients** are registered in the system.
2. **Products** (goods or services) are defined with pricing.
3. **Invoices** are issued to clients, each containing line items referencing products.
4. **Receipts** are recorded as payments arrive — receipt line items are allocated against specific invoices, automatically tracking outstanding balances.

The API is built with clean separation of concerns, uses a CQRS-inspired request/handler/response pattern, and ships with JWT authentication, rate limiting, and PDF generation out of the box.

---

## Key Features

- **Full CRUD operations** for Clients, Products, Invoices, and Receipts
- **Line item management** with upsert support (create or update in a single endpoint)
- **Payment allocation** — receipt items link directly to invoices, with automatic outstanding balance tracking
- **PDF generation** for invoices and receipts powered by QuestPDF
- **JWT authentication** with Bearer token support via `Microsoft.AspNetCore.Authentication.JwtBearer`
- **Password hashing** with BCrypt for secure credential storage
- **Rate limiting** middleware to protect against abuse
- **Swagger UI** for interactive API exploration and testing
- **AutoMapper** profiles for clean DTO-to-entity mapping
- **EF Core code-first migrations** with SQL Server

---

## Tech Stack

| Layer            | Technology                                  |
|------------------|---------------------------------------------|
| **Framework**    | ASP.NET Core 8.0                            |
| **Language**     | C# 12                                       |
| **ORM**          | Entity Framework Core 8.0                   |
| **Database**     | SQL Server                                  |
| **Auth**         | JWT Bearer Tokens + BCrypt password hashing |
| **Mapping**      | AutoMapper 16                               |
| **PDF Engine**   | QuestPDF 2024.10 (Community License)        |
| **API Docs**     | Swashbuckle / Swagger                       |

---

## Architecture

The project follows a **Request → Handler → Response** pattern inspired by CQRS, where each use case is encapsulated in its own folder with dedicated request, handler, and response types.

```
HTTP Request
    │
    ▼
┌──────────────┐
│  Controller   │   Routes & input validation
└──────┬───────┘
       │
       ▼
┌──────────────┐
│   Handler     │   Business logic (one per use case)
│  (UseCase)    │   Inherits from BaseHandler
└──────┬───────┘
       │
       ▼
┌──────────────┐
│  SQLContext    │   EF Core DbContext
│  (Data Layer) │   Entity configurations & migrations
└──────┬───────┘
       │
       ▼
   SQL Server
```

**Supporting layers:**
- **Services** — Cross-cutting logic like `AllocationService` (payment tracking) and `PdfService` (document generation)
- **Profiles** — AutoMapper mapping configurations between DTOs and entities
- **Infrastructure** — Authentication setup, DI registration, and rate limiting configuration
- **Extensions** — Service collection extension methods for clean `Program.cs` startup

---

## Getting Started

### Prerequisites

| Requirement                                                       | Version |
|-------------------------------------------------------------------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download)                 | 8.0+    |
| [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | 2019+   |
| [EF Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) | 8.0+    |

> **Note:** The EF Core CLI is required for running migrations. Install it globally with:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/reganmacnamara/BusinessManagementAPI.git
   cd BusinessManagementAPI
   ```

2. **Restore NuGet packages:**

   ```bash
   dotnet restore
   ```

### Database Setup

1. **Configure your connection string** in `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=MacsBusinessManagement;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

   Replace `YOUR_SERVER` with your SQL Server instance name (e.g., `localhost`, `.\SQLEXPRESS`, etc.).

2. **Apply EF Core migrations** to create the database schema:

   ```bash
   dotnet ef database update
   ```

### Running the API

```bash
dotnet run
```

Once the application starts, open your browser and navigate to:

```
https://localhost:{port}/swagger
```

The Swagger UI provides a full interactive reference for every endpoint, including request/response schemas and the ability to execute calls directly.

---

## Authentication

The API is secured with **JWT Bearer tokens**. All endpoints require a valid token in the `Authorization` header.

### Configuration

JWT settings are defined in `appsettings.json` under the `JwtSettings` section:

```json
{
  "JwtSettings": {
    "Issuer": "MacsBusinessManagementAPI",
    "Audience": "MacsBusinessManagementAPI",
    "SecretKey": "your-secret-key-here-min-32-characters"
  }
}
```

### Using Tokens

Include the token in your request headers:

```
Authorization: Bearer <your-jwt-token>
```

In Swagger UI, click the **Authorize** button (🔒) at the top of the page, enter your token, and all subsequent requests will include it automatically.

---

## API Reference

> **Interactive docs:** Run the API and visit `/swagger` for a live, testable reference with full schemas.

### Clients

| Method   | Route              | Description          |
|----------|--------------------|----------------------|
| `GET`    | `/Client`          | List all clients     |
| `GET`    | `/Client/{id}`     | Get a client by ID   |
| `POST`   | `/Client`          | Create a new client  |
| `PATCH`  | `/Client`          | Update a client      |
| `DELETE` | `/Client/{id}`     | Delete a client      |

### Products

| Method   | Route              | Description            |
|----------|--------------------|------------------------|
| `GET`    | `/Product`         | List all products      |
| `GET`    | `/Product/{id}`    | Get a product by ID    |
| `POST`   | `/Product`         | Create a new product   |
| `PATCH`  | `/Product`         | Update a product       |
| `DELETE` | `/Product/{id}`    | Delete a product       |

### Invoices

| Method   | Route                         | Description                            |
|----------|-------------------------------|----------------------------------------|
| `GET`    | `/Invoice`                    | List all invoices                      |
| `GET`    | `/Invoice/{id}`               | Get an invoice by ID                   |
| `GET`    | `/Invoice/Client/{clientId}`  | Get all invoices for a specific client |
| `GET`    | `/Invoice/{id}/pdf`           | Download invoice as a PDF              |
| `POST`   | `/Invoice`                    | Create a new invoice                   |
| `PATCH`  | `/Invoice`                    | Update an invoice                      |
| `PUT`    | `/Invoice/Item`               | Upsert an invoice line item            |
| `DELETE` | `/Invoice/{id}`               | Delete an invoice                      |
| `DELETE` | `/Invoice/Item/{id}`          | Delete an invoice line item            |

### Receipts

| Method   | Route                          | Description                                       |
|----------|--------------------------------|---------------------------------------------------|
| `GET`    | `/Receipt`                     | List all receipts                                 |
| `GET`    | `/Receipt/{id}`                | Get a receipt by ID                               |
| `GET`    | `/Receipt/Client/{clientId}`   | Get all receipts for a specific client            |
| `GET`    | `/Receipt/{id}/pdf`            | Download receipt as a PDF                         |
| `POST`   | `/Receipt`                     | Create a new receipt                              |
| `PATCH`  | `/Receipt`                     | Update a receipt                                  |
| `PUT`    | `/Receipt/Item`                | Upsert a receipt line item (allocates to invoice) |
| `DELETE` | `/Receipt/{id}`                | Delete a receipt                                  |
| `DELETE` | `/Receipt/Item/{id}`           | Delete a receipt line item                        |

---

## Core Concepts

### Invoice & Receipt Workflow

The API models a standard accounts-receivable workflow:

```
┌─────────┐     issues      ┌──────────┐
│  Client  │◄───────────────│  Invoice  │
└─────────┘                 └────┬─────┘
     │                           │ contains
     │                      ┌────▼──────────┐
     │                      │ Invoice Items  │  (product, qty, price)
     │                      └───────────────┘
     │
     │         pays          ┌──────────┐
     └──────────────────────►│  Receipt  │
                             └────┬─────┘
                                  │ contains
                             ┌────▼──────────┐
                             │ Receipt Items  │  (amount, allocated to invoice)
                             └───────────────┘
```

1. An **Invoice** is created for a client with one or more line items (each referencing a product, quantity, and price).
2. When the client pays, a **Receipt** is created. Each receipt line item specifies an amount and the invoice it is paying against.
3. The system automatically tracks how much of each invoice remains outstanding.

### Payment Allocation

The `AllocationService` handles the relationship between receipts and invoices. When a receipt item is created or updated via `PUT /Receipt/Item`, the service links the payment to the specified invoice and recalculates the outstanding balance. This ensures accurate, real-time tracking of what each client owes.

### PDF Generation

Both invoices and receipts can be exported as professionally formatted PDF documents via QuestPDF (Community License). The `PdfService` handles document composition and layout. Access the PDF endpoints at:

- `GET /Invoice/{id}/pdf`
- `GET /Receipt/{id}/pdf`

---

## Project Structure

```
BusinessManagementAPI/
│
├── Controllers/              # API controllers — route definitions, delegate to handlers
│   ├── ClientController.cs
│   ├── InvoiceController.cs
│   ├── ProductController.cs
│   └── ReceiptController.cs
│
├── UseCases/                 # Business logic, one folder per domain
│   ├── Clients/
│   │   ├── CreateClient/     # Request.cs, Handler.cs, Response.cs
│   │   ├── GetClient/
│   │   ├── GetClients/
│   │   ├── UpdateClient/
│   │   └── DeleteClient/
│   ├── Invoices/
│   │   └── ...               # Same pattern: Create, Get, Update, Delete, PDF, Items
│   ├── Products/
│   │   └── ...
│   └── Receipts/
│       └── ...
│
├── Entities/                 # Domain models (EF Core entities)
│   ├── Client.cs
│   ├── Product.cs
│   ├── Invoice.cs
│   ├── InvoiceItem.cs
│   ├── Receipt.cs
│   └── ReceiptItem.cs
│
├── Data/                     # EF Core DbContext and entity configurations
│   └── SQLContext.cs
│
├── Services/                 # Cross-cutting services
│   ├── AllocationService.cs  # Payment-to-invoice allocation logic
│   └── PdfService.cs         # Invoice & receipt PDF generation
│
├── Profiles/                 # AutoMapper mapping profiles
│
├── Infrastructure/           # Framework concerns
│   ├── Authentication/       # JWT configuration and setup
│   └── ServiceCollection/    # DI registration extensions
│
├── Extensions/               # Additional extension methods
│
├── Migrations/               # EF Core code-first migrations
│
├── Properties/               # Launch settings
│
├── Program.cs                # Application entry point & middleware pipeline
├── MacsBusinessManagementAPI.csproj
└── MacsBusinessManagementAPI.slnx
```

---

## Configuration

All configuration lives in `appsettings.json`. Key sections:

| Section              | Purpose                                               |
|----------------------|-------------------------------------------------------|
| `ConnectionStrings`  | SQL Server connection string (`DefaultConnection`)    |
| `JwtSettings`        | JWT issuer, audience, and secret key                  |

Environment-specific overrides can be placed in `appsettings.Development.json` or `appsettings.Production.json` following standard ASP.NET Core conventions.

---

## Contributing

Contributions are welcome! To get started:

1. **Fork** the repository
2. **Create a feature branch:** `git checkout -b feature/your-feature-name`
3. **Commit your changes:** `git commit -m "Add your feature description"`
4. **Push to your fork:** `git push origin feature/your-feature-name`
5. **Open a Pull Request** against the `main` branch

Please keep PRs focused on a single concern, follow the existing code conventions, and include a clear description of what your change does and why.

---

## Roadmap

> *This project is actively under development. Planned enhancements include:*

- [ ] User registration and login endpoints
- [ ] Role-based authorization (admin, accountant, viewer)
- [ ] Pagination, filtering, and sorting on list endpoints
- [ ] Client statement generation (full payment history as PDF)
- [ ] Overdue invoice notifications
- [ ] Unit and integration test suite
- [ ] Docker support and containerized deployment
- [ ] CI/CD pipeline via GitHub Actions

---

## License

This project is open source. See the repository for license details.

---

<p align="center">
  Built with ❤️ using ASP.NET Core 8 &nbsp;·&nbsp; <a href="https://github.com/reganmacnamara/BusinessManagementAPI">View on GitHub</a>
</p>