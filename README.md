<p align="center">
  <h1 align="center">MacsBusinessManagementAPI</h1>
  <p align="center">
    A clean, extensible, multi-tenant RESTful API for managing companies, clients, products, invoices, and receipts — built with ASP.NET Core 8 and Entity Framework Core.
  </p>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/EF_Core-8.0-512BD4?logo=dotnet&logoColor=white" alt="EF Core 8" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?logo=jsonwebtokens&logoColor=white" alt="JWT" />
  <img src="https://img.shields.io/badge/Hangfire-Jobs-EF3B2D" alt="Hangfire" />
  <img src="https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black" alt="Swagger" />
  <img src="https://img.shields.io/badge/License-MIT-blue" alt="License" />
</p>

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
  - [Request / Handler / Response Pattern](#request--handler--response-pattern)
  - [Entity Validation](#entity-validation)
  - [Multi-Tenancy](#multi-tenancy)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Secrets & Environment](#secrets--environment)
  - [Database Setup](#database-setup)
  - [Running the API](#running-the-api)
- [Authentication](#authentication)
- [Rate Limiting](#rate-limiting)
- [Background Jobs](#background-jobs)
- [API Reference](#api-reference)
  - [Auth](#auth)
  - [Companies](#companies)
  - [Company Settings](#company-settings)
  - [Clients](#clients)
  - [Products](#products)
  - [Payment Terms](#payment-terms)
  - [Invoices](#invoices)
  - [Receipts](#receipts)
- [Core Concepts](#core-concepts)
  - [Invoice & Receipt Workflow](#invoice--receipt-workflow)
  - [Payment Allocation](#payment-allocation)
  - [PDF Generation](#pdf-generation)
  - [Overdue Reminder Workflow](#overdue-reminder-workflow)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [Roadmap](#roadmap)
- [License](#license)

---

## Overview

**MacsBusinessManagementAPI** is a backend API for small-to-medium businesses that need to track clients, catalog products and services, issue invoices, record incoming payments, and chase overdue balances. It follows a straightforward real-world workflow:

1. **Register** an account and **authenticate** to receive a JWT token.
2. **Register a Company** — every subsequent entity is tenant-scoped to this company automatically.
3. **Configure Company Settings** (address, ABN, branding, invoice defaults, etc.).
4. **Clients** are registered in the system with optional payment terms and reminder intervals.
5. **Products** (goods or services) are defined with pricing.
6. **Invoices** are issued to clients, each containing line items referencing products.
7. **Receipts** are recorded as payments arrive — receipt line items are allocated against specific invoices, automatically tracking outstanding balances.
8. **Overdue invoice reminders** are dispatched daily by a Hangfire recurring job, respecting each client's configured reminder interval.

The API uses a CQRS-inspired Request / Handler / Response pattern with automatic handler registration, EF Core global query filters for tenant isolation, JWT authentication, per-user rate limiting, PDF generation, and Hangfire-powered scheduled jobs — all out of the box.

---

## Key Features

| Feature | Description |
|---------|-------------|
| **Multi-Tenancy** | Automatic tenant isolation via EF Core global query filters — every query is scoped to the authenticated user's company with zero handler code |
| **Auto-Stamped CompanyID** | `SaveChangesAsync` automatically stamps `CompanyID` on new entities, so Create handlers never need to set it manually |
| **Full CRUD** | Complete operations for Companies, Clients, Products, Invoices, Receipts, and Payment Terms |
| **Line Item Management** | Upsert support — create or update invoice and receipt line items in a single endpoint |
| **Payment Allocation** | Receipt items link directly to invoices with automatic outstanding balance tracking |
| **PDF Generation** | Professional invoice and receipt PDFs via QuestPDF |
| **JWT Authentication** | Secure Bearer token auth with BCrypt password hashing; `companyID` claim embedded in every token |
| **Rate Limiting** | Per-user and per-IP fixed-window rate limiting for authenticated and unauthenticated endpoints |
| **Entity Validation** | Dedicated `IUseCaseEntityValidator<T>` per use case, producing a uniform `EntityValidationResult` |
| **Auto-Registered Handlers** | Handlers, validators, and mediators are discovered and registered via reflection at startup |
| **Scheduled Jobs** | Hangfire-powered daily overdue-invoice reminder job with configurable per-client reminder intervals |
| **Email Delivery** | SMTP-based email service (Brevo / any SMTP provider) for automated reminder emails |
| **ABN Validation** | Built-in Australian Business Number validator |
| **Swagger UI** | Interactive API explorer with JWT authorization support |
| **Code-First Migrations** | EF Core migrations with Fluent API entity configurations |
| **.env Loader** | `.env` file is loaded into environment variables at startup for easy local secret management |

---

## Tech Stack

| Layer            | Technology                                  |
|------------------|---------------------------------------------|
| **Framework**    | ASP.NET Core 8.0                            |
| **Language**     | C# 12                                       |
| **ORM**          | Entity Framework Core 8.0                   |
| **Database**     | SQL Server                                  |
| **Auth**         | JWT Bearer Tokens + BCrypt password hashing |
| **Rate Limiting**| ASP.NET Core Rate Limiting middleware       |
| **Mapping**      | AutoMapper                                  |
| **PDF Engine**   | QuestPDF (Community License)                |
| **Jobs**         | Hangfire (SQL Server storage)               |
| **Email**        | SMTP (Brevo / any SMTP provider)            |
| **API Docs**     | Swashbuckle / Swagger                       |

---

## Architecture

### Request / Handler / Response Pattern

The project follows a **Request -> Validator -> Handler -> Response** pattern inspired by CQRS. Each use case is encapsulated in its own folder with a dedicated request, entity validator, handler, and response. Handlers implement `IUseCaseHandler<T>` and validators implement `IUseCaseEntityValidator<T>`; both are automatically discovered and registered in the DI container via reflection at startup.

```
HTTP Request
    |
    v
+----------------+
|  Controller    |   Routes, [Authorize], [EnableRateLimiting]
+-------+--------+
        |
        v
+----------------+
|  Mediator<T>   |   UseCaseMediator<TRequest>
+-------+--------+
        |
        v
+----------------+
| EntityValidator|   Confirms referenced entities exist (tenant-scoped)
|   <TRequest>   |   Returns EntityValidationResult
+-------+--------+
        |
        v
+----------------+
|   Handler      |   Business logic (one per use case)
| IUseCaseHandler|
+-------+--------+
        |
        v
+----------------+
|   SQLContext   |   EF Core DbContext with global query filters
|  (Data Layer)  |   Auto-stamps CompanyID on SaveChangesAsync
+-------+--------+
        |
        v
    SQL Server
```

### Entity Validation

Every use case has a dedicated validator implementing `IUseCaseEntityValidator<TRequest>`. Validators run **before** handlers via `UseCaseMediator<T>` and produce a uniform `EntityValidationResult`:

```csharp
public async Task<EntityValidationResult> ValidateAsync(
    UpdateClientRequest request, CancellationToken cancellationToken)
{
    if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
        return EntityValidationResult.Failure(nameof(Client), request.ClientID);

    return EntityValidationResult.Success();
}
```

Failures return a structured error response; the handler is never invoked if validation fails.

### Multi-Tenancy

All tenant-scoped entities (`Account`, `Client`, `Invoice`, `Product`, `PaymentTerm`, `Receipt`) carry a `CompanyID` foreign key. Tenant isolation is enforced at the data layer — handlers never need to filter by `CompanyID` themselves:

| Mechanism | Where | Purpose |
|-----------|-------|---------|
| `ITenantProvider` | `Infrastructure/Authentication/TenantProvider.cs` | Reads `companyID` and `sub` claims from the current JWT |
| Global Query Filters | `Data/SQLContext.OnModelCreating` | `HasQueryFilter(e => e.CompanyID == CompanyID)` on every tenant-scoped entity |
| Auto-stamping | `SQLContext.SaveChangesAsync` | Sets `CompanyID` on newly added entities automatically |
| `IgnoreQueryFilters()` | Hangfire jobs, login lookup | Explicit opt-out for cross-tenant operations |

Result: every query is automatically scoped to the caller's company with zero boilerplate in handlers.

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

### Secrets & Environment

On startup, `Program.cs` loads a `.env` file from the working directory (if present) and copies each `KEY=VALUE` line into the process environment **before** the configuration system builds. This means you can reference any of these values in `appsettings.json` via the normal `${ENV_VAR}` / environment variable override pattern.

Create a `.env` file in the project root for local development (do **not** commit it):

```env
ConnectionStrings__DefaultConnection=Server=localhost;Database=MacsBusinessManagement;Trusted_Connection=True;TrustServerCertificate=True;
JwtSettings__Secret=your-local-development-secret-key-min-32-characters
SmtpSettings__Host=smtp-relay.brevo.com
SmtpSettings__Port=587
SmtpSettings__Username=your-brevo-login
SmtpSettings__Password=your-brevo-smtp-key
SmtpSettings__FromAddress=noreply@yourdomain.com
SmtpSettings__FromName=Your Business Name
```

> **Security:** Never commit `.env`. In production, use a real secret store (Azure Key Vault, AWS Secrets Manager, GitHub Actions secrets, etc.) — the `.env` loader is intended for local development and CI only.

### Database Setup

1. **Configure your connection string** via `.env` (preferred) or `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=MacsBusinessManagement;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

2. **Apply migrations:**

   ```bash
   dotnet ef database update
   ```

   This will create both the application schema and the Hangfire schema in the target database.

### Running the API

```bash
dotnet run
```

Navigate to `https://localhost:{port}/swagger` to access the interactive API documentation, or `https://localhost:{port}/hangfire` to view the Hangfire dashboard.

---

## Authentication

The API uses **JWT Bearer tokens**. All endpoints except `/Auth/Login` and `/Auth/Register` require a valid token.

### Workflow

```
POST /Auth/Register       ->   Create an account (email + password)
POST /Auth/Login          ->   Receive a JWT token (with companyID claim)
POST /Company/Register    ->   Create a company for the account (first-time setup)
Use token in headers      ->   Access tenant-scoped protected endpoints
```

### JWT Claims

Each issued token includes:

| Claim | Meaning |
|-------|---------|
| `sub` | Account ID (authenticated user) |
| `email` | Account email |
| `companyID` | Tenant ID used by `ITenantProvider` to scope every query |
| `jti` | Unique token ID |

### JWT Configuration

JWT settings live in `appsettings.json` (or `.env` overrides):

```json
{
  "JwtSettings": {
    "Issuer": "MacsBusinessManagementAPI",
    "Audience": "MacsBusinessManagementAPI",
    "Secret": "your-secret-key-here-min-32-characters",
    "ExpiryMinutes": 60
  }
}
```

### Using Tokens

Include the token in the `Authorization` header:

```
Authorization: Bearer <your-jwt-token>
```

In Swagger UI, click the **Authorize** button at the top of the page, paste your token, and all subsequent requests will include it automatically.

---

## Rate Limiting

The API enforces per-partition fixed-window rate limits to protect against abuse. Policies are defined in `ServiceCollectionExtensions.AddRateLimiting()`.

| Policy | Partition Key | Limit | Window | Applied To |
|--------|--------------|-------|--------|------------|
| `Authenticated` | User ID (from JWT) | 60 requests | 60 seconds | All protected endpoints |
| `Unauthenticated` | IP address | 5 requests | 60 seconds | Auth endpoints (login, register) |

Exceeding the limit returns `429 Too Many Requests`. Each user / IP gets their own independent bucket — one client's usage does not affect another's.

---

## Background Jobs

The API uses **Hangfire** (SQL Server storage) for scheduled and background work. The Hangfire dashboard is mounted at `/hangfire`.

| Job | Schedule | Purpose |
|-----|----------|---------|
| `overdue-invoice-reminders` | Daily at 07:00 UTC | Iterates all companies, finds overdue invoices per client, respects each client's `ReminderIntervalDays`, sends an SMTP reminder, and logs the send in `ReminderLog` |

The overdue-reminder job iterates across **all** tenants, so it opts out of the global query filters via `IgnoreQueryFilters()` on its queries. Re-send frequency per client is governed by `Client.ReminderIntervalDays` (set to `0` to disable reminders for that client) and the most recent `ReminderLog` row.

---

## API Reference

> **Interactive docs:** Run the API and visit `/swagger` for a live, testable reference with full request / response schemas.

### Auth

| Method | Route            | Description                          | Rate Limit      |
|--------|------------------|--------------------------------------|-----------------|
| `POST` | `/Auth/Register` | Create a new account                 | Unauthenticated |
| `POST` | `/Auth/Login`    | Authenticate and receive a JWT token | Unauthenticated |

### Companies

| Method | Route               | Description                                   |
|--------|---------------------|-----------------------------------------------|
| `GET`  | `/Company`          | Get the authenticated user's company          |
| `POST` | `/Company`          | Update the authenticated user's company       |
| `POST` | `/Company/Register` | Register a new company for the account (first-time onboarding) |

### Company Settings

| Method | Route              | Description                                        |
|--------|--------------------|----------------------------------------------------|
| `GET`  | `/CompanySettings` | Get company settings (branding, defaults, ABN)     |
| `POST` | `/CompanySettings` | Upsert company settings                            |

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

### Payment Terms

| Method   | Route                         | Description                         |
|----------|-------------------------------|-------------------------------------|
| `GET`    | `/PaymentTerms`               | List all payment terms              |
| `GET`    | `/PaymentTerms/{id}`          | Get a payment term by ID            |
| `POST`   | `/PaymentTerms`               | Create a new payment term           |
| `PATCH`  | `/PaymentTerms`               | Update a payment term               |
| `DELETE` | `/PaymentTerms/{id}`          | Delete a payment term               |

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
|  Client |<----------------|  Invoice |
+---------+                 +----+-----+
     |                           | contains
     |                      +----v----------+
     |                      | Invoice Items |  (product, qty, price)
     |                      +---------------+
     |
     |         pays          +----------+
     +---------------------->|  Receipt |
                             +----+-----+
                                  | contains
                             +----v----------+
                             | Receipt Items |  (amount, allocated to invoice)
                             +---------------+
```

1. An **Invoice** is created for a client with one or more line items (each referencing a product, quantity, and price).
2. When the client pays, a **Receipt** is created. Each receipt line item specifies an amount and the invoice it is paying against.
3. The system automatically tracks how much of each invoice remains outstanding.

### Payment Allocation

The `AllocationService` handles the relationship between receipts and invoices. When a receipt item is created or updated via `PUT /Receipt/Item`, the service links the payment to the specified invoice and recalculates the outstanding balance. `Invoice.Outstanding` flips to `false` when the remaining balance reaches zero.

### PDF Generation

Both invoices and receipts can be exported as professionally formatted PDF documents via QuestPDF (Community License). The `PdfService` handles document composition and layout, using the company settings (ABN, logo, address) to brand the output.

- `GET /Invoice/{id}/pdf`
- `GET /Receipt/{id}/pdf`

### Overdue Reminder Workflow

Run daily by Hangfire:

```
For each Company (IgnoreQueryFilters):
    For each Client with ReminderIntervalDays > 0 and an email:
        Look up the most recent ReminderLog for this client
        If the last reminder is within ReminderIntervalDays, skip
        Find all outstanding invoices past their DueDate
        If any exist:
            Send a reminder email via SmtpEmailService
            Write a ReminderLog row
```

Reminder cadence is per-client and fully controlled by `Client.ReminderIntervalDays`.

---

## Project Structure

```
MacsBusinessManagementAPI/
|
+-- Controllers/                     # API controllers
|   +-- AuthController.cs            # Registration & login (AllowAnonymous)
|   +-- CompanyController.cs         # Company profile + first-time registration
|   +-- CompanySettingsController.cs # Company-level settings (branding, defaults)
|   +-- ClientController.cs          # Client CRUD
|   +-- InvoiceController.cs         # Invoice CRUD, line items, PDF
|   +-- PaymentTermsController.cs    # PaymentTerm CRUD
|   +-- ProductController.cs         # Product CRUD
|   +-- ReceiptController.cs         # Receipt CRUD, line items, PDF
|
+-- UseCases/                        # Business logic (one folder per use case)
|   +-- Auth/                        # Login, Register
|   +-- Companies/                   # GetCompany, RegisterCompany, UpdateCompanyDetails
|   +-- CompanySettings/             # GetCompanySettings, UpsertCompanySettings
|   +-- Clients/                     # CreateClient, GetClient(s), UpdateClient, DeleteClient
|   +-- Invoices/                    # Full CRUD + UpsertInvoiceItem, GetInvoicePdf, GetClientInvoices
|   +-- PaymentTerms/                # Full CRUD
|   +-- Products/                    # Full CRUD
|   +-- Receipts/                    # Full CRUD + UpsertReceiptItem, GetReceiptPdf, GetClientReceipts
|
+-- Entities/                        # EF Core entities
|   +-- Account.cs, Company.cs, CompanySettings.cs
|   +-- Client.cs, PaymentTerm.cs, Product.cs
|   +-- Invoice.cs, InvoiceItem.cs
|   +-- Receipt.cs, ReceiptItem.cs
|   +-- ReminderLog.cs
|
+-- Data/
|   +-- SQLContext.cs                # DbContext — global query filters, auto-stamping, GetEntities<T>()
|   +-- Configurations/              # Fluent API entity configurations
|
+-- Infrastructure/
|   +-- ABNValidator/                # Australian Business Number validator
|   +-- Authentication/              # JwtConfig, ITenantProvider, TenantProvider
|   +-- EntityValidator/             # EntityValidator, EntityValidationResult
|   +-- Jobs/                        # OverdueInvoiceReminderJob (Hangfire)
|   +-- Pipeline/                    # IUseCaseHandler<T>, IUseCaseEntityValidator<T>, UseCaseMediator<T>, IUseCaseRequest
|   +-- ServiceCollection/           # DI extensions (services, JWT, rate limiting, Hangfire, handler registration)
|   +-- Services/
|       +-- Allocations/             # AllocationService — payment-to-invoice allocation
|       +-- Auth/                    # AuthService — JWT + BCrypt
|       +-- DueDate/                 # DueDateCalculator — payment-term-driven due dates
|       +-- Email/                   # SmtpEmailService — reminder emails
|       +-- Pdf/                     # PdfService — invoice & receipt PDFs
|
+-- Profiles/                        # AutoMapper mapping profiles
+-- Migrations/                      # EF Core code-first migrations
+-- Program.cs                       # Entry point: .env loader, DI, middleware, Hangfire recurring jobs
```

---

## Configuration

Configuration is layered: `appsettings.json` -> `appsettings.{Environment}.json` -> environment variables (including those loaded from `.env`).

| Section                      | Purpose                                                          |
|------------------------------|------------------------------------------------------------------|
| `ConnectionStrings`          | SQL Server connection string (`DefaultConnection`) — used by both EF Core and Hangfire |
| `JwtSettings`                | JWT `Issuer`, `Audience`, `Secret`, `ExpiryMinutes`              |
| `SmtpSettings`               | SMTP host, port, credentials, and from-address for reminder emails |

Environment-specific overrides can be placed in `appsettings.Development.json` or `appsettings.Production.json` following standard ASP.NET Core conventions. For local development, prefer the `.env` file — it is loaded automatically on startup.

---

## Contributing

Contributions are welcome! To get started:

1. **Fork** the repository
2. **Create a feature branch:** `git checkout -b feature/your-feature-name`
3. **Commit your changes:** `git commit -m "Add your feature description"`
4. **Push to your fork:** `git push origin feature/your-feature-name`
5. **Open a Pull Request** against the `main` branch

Please keep PRs focused on a single concern, follow the existing code conventions (Request / Validator / Handler / Response per use case), and include a clear description of what your change does and why.

---

## Roadmap

> *This project is actively under development. Planned enhancements include:*

- [x] JWT authentication with registration and login
- [x] Rate limiting middleware
- [x] Use case handler auto-registration via reflection
- [x] Uniform entity validation via `IUseCaseEntityValidator<T>` + `EntityValidationResult`
- [x] Multi-tenancy via EF Core global query filters
- [x] Auto-stamping of `CompanyID` on create
- [x] Hangfire recurring jobs + dashboard
- [x] Overdue invoice email reminders (per-client interval)
- [x] Company profile and company settings endpoints
- [x] Payment terms management
- [x] ABN validation
- [ ] Role-based authorization (admin, accountant, viewer)
- [ ] Refresh tokens
- [ ] Pagination, filtering, and sorting on list endpoints
- [ ] Client statement generation (full payment history as PDF)
- [ ] Webhooks for invoice / receipt lifecycle events
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
