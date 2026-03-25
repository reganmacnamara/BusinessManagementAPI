<p align="center">
  <h1 align="center">MacsBusinessManagementAPI</h1>
  <p align="center">
    A clean, extensible RESTful API for managing clients, products, invoices, and receipts — built with ASP.NET Core 8 and Entity Framework Core.
  </p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/EF_Core-8.0-512BD4?logo=dotnet&logoColor=white" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?logo=jsonwebtokens&logoColor=white" alt="JWT" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black" alt="Swagger" />
  <img src="https://img.shields.io/badge/License-MIT-blue" alt="License" />
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
- [Rate Limiting](#rate-limiting)
- [API Reference](#api-reference)
  - [Auth](#auth)
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

1. **Register** an account and **authenticate** to receive a JWT token.
2. **Clients** are registered in the system.
3. **Products** (goods or services) are defined with pricing.
4. **Invoices** are issued to clients, each containing line items referencing products.
5. **Receipts** are recorded as payments arrive — receipt line items are allocated against specific invoices, automatically tracking outstanding balances.

The API uses a CQRS-inspired Request/Handler/Response pattern with automatic handler registration, and ships with JWT authentication, per-user rate limiting, and PDF generation out of the box.

---

## Key Features

| Feature | Description |
|---------|-------------|
| **Full CRUD** | Complete operations for Clients, Products, Invoices, and Receipts |
| **Line Item Management** | Upsert support — create or update line items in a single endpoint |
| **Payment Allocation** | Receipt items link directly to invoices with automatic outstanding balance tracking |
| **PDF Generation** | Professional invoice and receipt PDFs via QuestPDF |
| **JWT Authentication** | Secure Bearer token auth with BCrypt password hashing |
| **Rate Limiting** | Per-user and per-IP rate limiting policies for authenticated and unauthenticated endpoints |
| **Auto-Registered Handlers** | Use case handlers are discovered and registered via reflection at startup |
| **Swagger UI** | Interactive API explorer with JWT authorization support |
| **Code-First Migrations** | EF Core migrations with Fluent API entity configurations |

---

## Tech Stack

| Layer            | Technology                                  |
|------------------|---------------------------------------------|
| **Framework**    | ASP.NET Core 8.0                            |
| **Language**     | C# 12                                       |
| **ORM**          | Entity Framework Core 8.0                   |
| **Database**     | SQL Server                                  |
| **Auth**         | JWT Bearer Tokens + BCrypt password hashing |
| **Rate Limiting**| ASP.NET Core Rate Limiting middleware        |
| **Mapping**      | AutoMapper 16                               |
| **PDF Engine**   | QuestPDF 2024.10 (Community License)        |
| **API Docs**     | Swashbuckle / Swagger                       |

---

## Architecture

The project follows a **Request -> Handler -> Response** pattern inspired by CQRS. Each use case is encapsulated in its own folder with a dedicated request, handler, and response. Handlers implement `IUseCaseHandler<T>` and are automatically discovered and registered in the DI container via reflection at startup.

```
HTTP Request
    |
    v
+--------------+
|  Controller   |   Routes, [Authorize], [EnableRateLimiting]
+------+-------+
       |
       v
+--------------+
|   Handler     |   Business logic (one per use case)
| IUseCaseHandler<T>
+------+-------+
       |
       v
+--------------+
|  SQLContext    |   EF Core DbContext
|  (Data Layer) |   Fluent API configurations
+------+-------+
       |
       v
   SQL Server
```

**Supporting layers:**

| Layer | Purpose |
|-------|---------|
| **Infrastructure/Pipeline** | `IUseCaseHandler<T>`, `IUseCaseRequest`, `ExistenceChecker<T>` |
| **Infrastructure/Authentication** | JWT config, token generation, password hashing |
| **Infrastructure/ServiceCollection** | Extension methods for DI registration |
| **Services** | Cross-cutting logic — `AllocationService`, `PdfService` |
| **Profiles** | AutoMapper mapping configurations |

---

## Getting Started

### Prerequisites

| Requirement                                                       | Version |
|-------------------------------------------------------------------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download)                 | 8.0+    |
| [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | 2019+   |
| [EF Core CLI Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) | 8.0+    |

> **Note:** Install the EF Core CLI globally with:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### Installation

```bash
git clone https://github.com/reganmacnamara/InvoiceAutomationAPI.git
cd InvoiceAutomationAPI
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

   Replace `YOUR_SERVER` with your SQL Server instance (e.g., `localhost`, `.\SQLEXPRESS`).

2. **Apply migrations:**

   ```bash
   dotnet ef database update
   ```

### Running the API

```bash
dotnet run
```

Navigate to `https://localhost:{port}/swagger` to access the interactive API documentation.

---

## Authentication

The API uses **JWT Bearer tokens**. All endpoints except registration and login require a valid token.

### Workflow

```
POST /Auth/Register       ->    Create an account (email + password)
POST /Auth/Login          ->    Receive a JWT token
Use token in headers      ->    Access protected endpoints
```

### JWT Configuration

JWT settings are defined in `appsettings.json`:

```json
{
  "JwtSettings": {
    "Issuer": "MacsBusinessManagementAPI",
    "Audience": "MacsBusinessManagementAPI",
    "Secret": "your-secret-key-here-min-32-characters"
  }
}
```

> **Security:** In production, store the secret in environment variables or user secrets — never commit it to source control.

### Using Tokens

Include the token in the `Authorization` header:

```
Authorization: Bearer <your-jwt-token>
```

In Swagger UI, click the **Authorize** button at the top of the page, paste your token, and all subsequent requests will include it automatically.

---

## Rate Limiting

The API enforces per-partition rate limits to protect against abuse. Policies are defined in `ServiceCollectionExtensions.AddRateLimiting()`.

| Policy | Partition Key | Limit | Window | Applied To |
|--------|--------------|-------|--------|------------|
| `Authenticated` | User ID (from JWT) | 10 requests | 12 seconds | All protected endpoints |
| `Unauthenticated` | IP address | 3 requests | 30 seconds | Auth endpoints (login, register) |

Exceeding the limit returns `429 Too Many Requests`. Each user/IP gets their own independent bucket — one client's usage does not affect another's.

---

## API Reference

> **Interactive docs:** Run the API and visit `/swagger` for a live, testable reference with full request/response schemas.

### Auth

| Method | Route | Description | Rate Limit |
|--------|-------|-------------|------------|
| `POST` | `/Auth/Register` | Create a new account | Unauthenticated |
| `POST` | `/Auth/Login` | Authenticate and receive a JWT token | Unauthenticated |

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
| `GET`    | `/Invoice/{id}/pdf`           | Download invoice as PDF                |
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
| `GET`    | `/Receipt/{id}/pdf`            | Download receipt as PDF                           |
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
+---------+     issues      +----------+
|  Client  |<---------------|  Invoice  |
+---------+                 +----+-----+
     |                           | contains
     |                      +----v----------+
     |                      | Invoice Items  |  (product, qty, price)
     |                      +---------------+
     |
     |         pays          +----------+
     +---------------------->|  Receipt  |
                             +----+-----+
                                  | contains
                             +----v----------+
                             | Receipt Items  |  (amount, allocated to invoice)
                             +---------------+
```

1. An **Invoice** is created for a client with one or more line items (each referencing a product, quantity, and price).
2. When the client pays, a **Receipt** is created. Each receipt line item specifies an amount and the invoice it is paying against.
3. The system automatically tracks how much of each invoice remains outstanding.

### Payment Allocation

The `AllocationService` handles the relationship between receipts and invoices. When a receipt item is created or updated via `PUT /Receipt/Item`, the service links the payment to the specified invoice and recalculates the outstanding balance. This ensures accurate, real-time tracking of what each client owes.

### PDF Generation

Both invoices and receipts can be exported as professionally formatted PDF documents via QuestPDF (Community License). The `PdfService` handles document composition and layout.

- `GET /Invoice/{id}/pdf`
- `GET /Receipt/{id}/pdf`

---

## Project Structure

```
MacsBusinessManagementAPI/
|
+-- Controllers/                 # API controllers
|   +-- AuthController.cs        # Registration & login (AllowAnonymous)
|   +-- ClientController.cs      # Client CRUD
|   +-- InvoiceController.cs     # Invoice CRUD, line items, PDF
|   +-- ProductController.cs     # Product CRUD
|   +-- ReceiptController.cs     # Receipt CRUD, line items, PDF
|
+-- UseCases/                    # Business logic (one folder per use case)
|   +-- Auth/
|   |   +-- Login/               # LoginAccountRequest, Handler, Response
|   |   +-- Register/            # RegisterAccountRequest, Handler, Response
|   +-- Clients/
|   |   +-- CreateClient/        # Request, Handler, Response
|   |   +-- GetClient/
|   |   +-- GetClients/
|   |   +-- UpdateClient/
|   |   +-- DeleteClient/
|   +-- Invoices/                # Same pattern + GetInvoicePdf, UpsertInvoiceItem, etc.
|   +-- Products/
|   +-- Receipts/
|
+-- Entities/                    # EF Core entities
|   +-- Account.cs
|   +-- Client.cs
|   +-- Product.cs
|   +-- Invoice.cs, InvoiceItem.cs
|   +-- Receipt.cs, ReceiptItem.cs
|
+-- Data/
|   +-- SQLContext.cs            # DbContext with generic GetEntities<T>()
|   +-- Configurations/         # Fluent API entity configurations
|
+-- Infrastructure/
|   +-- Authentication/          # JwtConfig, AuthService, IAuthService
|   +-- Pipeline/                # IUseCaseHandler<T>, IUseCaseRequest, ExistenceChecker<T>
|   +-- ServiceCollection/       # DI extensions (services, JWT, rate limiting, handler registration)
|
+-- Services/
|   +-- Allocations/             # AllocationService — payment-to-invoice allocation
|   +-- Pdf/                     # PdfService — invoice & receipt PDF generation
|
+-- Profiles/                    # AutoMapper mapping profiles
+-- Migrations/                  # EF Core code-first migrations
+-- Program.cs                   # Entry point & middleware pipeline
```

---

## Configuration

All configuration lives in `appsettings.json`:

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

- [x] JWT authentication with registration and login
- [x] Rate limiting middleware
- [x] Use case handler auto-registration via reflection
- [ ] Role-based authorization (admin, accountant, viewer)
- [ ] Refresh tokens
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
  Built with ASP.NET Core 8 &nbsp;&middot;&nbsp; <a href="https://github.com/reganmacnamara/InvoiceAutomationAPI">View on GitHub</a>
</p>
