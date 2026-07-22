# BulkyStore - ASP.NET 7 E-Commerce MVC Application

A full-featured e-commerce platform built with ASP.NET 7, demonstrating modern architectural patterns and best practices. BulkyStore provides a complete shopping experience with product browsing, cart management, order processing, and secure payment integration.

## Stack

- **Language(s):** C# (62.4%), HTML (32.8%), CSS (3%), JavaScript (1.8%)
- **Framework:** ASP.NET 7 MVC & Razor Pages
- **Notable Libraries & Integrations:**
  - **Entity Framework Core** - ORM for database operations
  - **Stripe Payment Gateway** - Secure payment processing
  - **SendGrid** - Email service for notifications
  - **ASP.NET Core Identity** - User authentication and authorization

## How it's organized

```
BulkyStore/
├── BulkyStore_UI/                 Main MVC web application (entry point)
│   ├── Areas/                      Admin and Customer area controllers
│   ├── Controllers/                MVC controllers
│   ├── Views/                      Razor view templates
│   ├── ViewComponents/             Reusable view components
│   ├── Program.cs                  Application startup configuration
│   ├── appsettings.json            Configuration (database, Stripe, SendGrid)
│   └── wwwroot/                    Static files (CSS, JS, images)
│
├── BulkyStore_UI_Razor/            Alternative Razor Pages UI implementation
│
├── BulkyStore_DataAccess/          Data access layer (repository pattern)
│   ├── Data/                       DbContext and entity configurations
│   ├── Repository/                 Concrete repository implementations
│   │   ├── IRepository/            Repository interfaces (abstraction)
│   │   ├── UnitOfWork.cs          Coordinates multiple repositories
│   │   └── [Entity]Repository.cs  Category, Product, Order, User repos
│   ├── Migrations/                 Entity Framework migrations
│   └── DbInitializer/              Database seeding logic
│
├── BulkyStore_Models/              Domain models and view models
│   ├── Models/                     Database entities (Product, Category, Order, etc.)
│   └── ViewModels/                 View-specific data transfer objects
│
├── BulkyStore_Utility/             Cross-cutting concerns
│   ├── SD.cs                       Static constants and enumerations
│   ├── EmailSender.cs              Email notification logic
│   └── StripeSettings.cs           Stripe configuration class
│
├── BulkyStore_Extensions/          Dependency injection and configuration extensions
│   ├── BaseExtension.cs            Base service registrations
│   ├── AuthenticationExtension.cs  Identity and authentication setup
│   ├── DataBaseExtension.cs        EF Core and connection configuration
│   ├── CookieExtension.cs          Session and cookie management
│   ├── SessionExtension.cs         Session state configuration
│   └── StripePaymentExtension.cs   Stripe integration setup
│
└── BulkyStore.sln                  Visual Studio solution file
```

## How it fits together

**Request Flow:** 
1. Incoming HTTP requests route to MVC controllers in `BulkyStore_UI/Areas/` (Admin or Customer)
2. Controllers inject `IUnitOfWork` to access repositories
3. Repositories (via `Repository.cs` base class) perform CRUD operations using Entity Framework Core
4. DbContext queries against SQL Server database
5. Models pass through View Models for presentation logic
6. Responses render via Razor views with HTML/CSS/JavaScript

**Data Flow:**
- **Shopping:** User browses products → CartItem saved to session → Order created in database
- **Payments:** Stripe integration processes transactions via `StripePaymentExtension`
- **Notifications:** `EmailSender` via SendGrid notifies users of order status changes
- **Authentication:** ASP.NET Identity manages user accounts and authorization

## How to run it

### Prerequisites
- **.NET 7 SDK** or later
- **SQL Server** 2016+ (local or remote)
- **Stripe Account** (for payment testing)
- **SendGrid Account** (for email notifications)

### Setup Steps

1. **Clone and Open**
   ```bash
   git clone https://github.com/avijitg11/dotnet-core-mvc-ecommerce.git
   cd dotnet-core-mvc-ecommerce/BulkyStore
   ```

2. **Configure Database Connection**
   
   Edit `BulkyStore_UI/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "BulkyStoreConnectionString": "Data Source=YOUR_SERVER;Initial Catalog=BulkyStore;Integrated Security=True;TrustServerCertificate=True;"
   }
   ```

3. **Add Payment & Email Keys**
   
   In `BulkyStore_UI/appsettings.json`:
   ```json
   "Stripe": {
     "SecretKey": "your_stripe_secret_key",
     "PublishableKey": "your_stripe_publishable_key"
   },
   "SendGrid": {
     "SecretKey": "your_sendgrid_api_key"
   }
   ```

4. **Apply Migrations & Seed Database**
   ```bash
   dotnet ef database update --project BulkyStore_DataAccess --startup-project BulkyStore_UI
   ```
   This runs migrations and seeds initial data (categories, products, users) via `DbInitializer`

5. **Run the Application**
   ```bash
   cd BulkyStore_UI
   dotnet run
   ```
   
   Application opens at `https://localhost:7263` (check console for actual port)

### Project Structure - Two Entry Points

- **BulkyStore_UI** - Default MVC application with Areas-based admin and customer sections
- **BulkyStore_UI_Razor** - Alternative Razor Pages implementation (same backend services)

Both share the same `BulkyStore_DataAccess` layer, allowing you to use either UI interchangeably.

## Key Features

✅ **Product Catalog** - Browse categories, view product details, manage inventory  
✅ **Shopping Cart** - Add/remove items, persist via session  
✅ **User Authentication** - Register, login, manage profiles via ASP.NET Identity  
✅ **Order Management** - Place orders, track status, view order history  
✅ **Admin Dashboard** - Manage products, categories, companies, orders, and users  
✅ **Secure Payments** - Stripe integration for credit card transactions  
✅ **Email Notifications** - SendGrid integration for order confirmations and status updates  
✅ **Repository Pattern** - Clean separation of concerns with abstraction via interfaces  
✅ **Unit of Work Pattern** - Coordinate multiple repositories with transactional boundaries  

## Technology Highlights

| Component | Technology |
|-----------|-----------|
| Web Framework | ASP.NET 7 MVC + Razor Pages |
| ORM | Entity Framework Core 7 |
| Authentication | ASP.NET Core Identity |
| Database | SQL Server |
| Payment Gateway | Stripe API |
| Email Service | SendGrid API |
| Session Management | In-memory session state |
| Configuration | Dependency Injection with IServiceCollection |

## Architecture Patterns

- **Repository Pattern** - Data access abstraction via `IRepository<T>` and concrete implementations
- **Unit of Work Pattern** - `IUnitOfWork` coordinates repositories and manages transactions
- **Dependency Injection** - Extension methods register services (Auth, Database, Stripe, etc.)
- **Areas** - Organization of Admin and Customer concerns
- **View Components** - Reusable UI logic encapsulation
- **View Models** - Separation of domain models from presentation

## Development Notes

- Migrations are located in `BulkyStore_DataAccess/Migrations/`
- Add new entities in `BulkyStore_Models/Models/`, then create migrations
- Use `DbInitializer.cs` to seed reference data (categories, companies, roles)
- Session-based shopping cart implemented via `ShoppingCartRepository`
- Stripe keys must be configured in `appsettings.json` for payment features
- Email functionality requires active SendGrid API key

## Testing & Debugging

Open the solution in **Visual Studio 2022** or **Visual Studio Code**:
```bash
# Using VS Code
code .

# Using Visual Studio
start BulkyStore.sln
```

Set breakpoints in repository methods or controllers to trace data flow.

## License

Open source for educational purposes.

---

**Last Updated:** February 2026  
**Built with:** ASP.NET 7, Entity Framework Core, Stripe, SendGrid
