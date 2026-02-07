# Blog API

A RESTful API for a blogging platform built with ASP.NET Core 6.0 following Clean Architecture principles and Domain-Driven Design (DDD) patterns.

## 🏗️ Architecture

This project implements **Clean Architecture** with clear separation of concerns across four layers:

```
Blog/
├── Blog.Domain/          # Enterprise business rules and entities
├── Blog.Application/     # Application business rules and DTOs
├── Blog.Infrastructure/  # External concerns (Database, External Services)
└── Blog.API/            # Presentation layer (Controllers, Middleware)
```

### Layers Overview

- **Blog.Domain**: Contains core business entities (Post, Comment, Category), interfaces, and specifications
- **Blog.Application**: Houses service implementations, DTOs, AutoMapper profiles, and application interfaces
- **Blog.Infrastructure**: Implements data access using Entity Framework Core, repositories, and external service clients
- **Blog.API**: ASP.NET Core Web API with controllers, middleware, and authentication handlers

## ✨ Features

- **Post Management**: Create, read, update, and delete blog posts
- **Comment System**: Add and manage comments on posts
- **Category Organization**: Organize posts by categories
- **Authentication & Authorization**: Custom bearer token authentication with external auth service integration
- **Repository Pattern**: Generic repository with Unit of Work pattern
- **Specification Pattern**: Query specification for complex data retrieval
- **AutoMapper**: Object-to-object mapping for DTOs
- **Swagger/OpenAPI**: API documentation and testing interface

## 🛠️ Technologies

- **.NET 6.0**
- **ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server)
- **AutoMapper**
- **Swagger/OpenAPI**
- **Custom Authentication Handler**

## 📋 Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or Visual Studio Code

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/KamalElsayedJR/Blog.git
cd Blog
```

### 2. Configure Connection String

Update the connection string in `Blog.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection=True;"
  },
  "AuthApi": {
    "baseUrl": "https://your-auth-service-url"
  }
}
```

### 3. Apply Database Migrations

```bash
cd Blog.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../Blog.API
dotnet ef database update --startup-project ../Blog.API
```

### 4. Run the Application

```bash
cd Blog.API
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7xxx`
- HTTP: `http://localhost:5xxx`
- Swagger UI: `https://localhost:7xxx/swagger`

## 📚 API Endpoints

### Posts

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/post` | Get all posts with comments | No |
| GET | `/api/post/{id}` | Get post by ID | No |
| POST | `/api/post` | Create new post | Yes |
| PUT | `/api/post/{id}` | Update post | Yes |
| DELETE | `/api/post/{id}` | Delete post | Yes |

### Comments

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/comment/{postId}` | Get comments for a post | No |
| POST | `/api/comment` | Add comment to post | Yes |
| DELETE | `/api/comment/{id}` | Delete comment | Yes |

### Categories

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/category` | Get all categories | No |
| GET | `/api/category/{id}` | Get category by ID | No |
| POST | `/api/category` | Create new category | Yes |
| PUT | `/api/category/{id}` | Update category | Yes |
| DELETE | `/api/category/{id}` | Delete category | Yes |

## 🏛️ Project Structure

### Domain Layer

```
Blog.Domain/
├── Entities/
│   ├── Post.cs
│   ├── Comment.cs
│   └── Category.cs
├── Interfaces/
│   ├── IGenericRepository.cs
│   ├── ICategoryRepository.cs
│   └── IUnitOfWork.cs
└── Specifications/
    ├── ISpecification.cs
    ├── BaseSpecification.cs
    └── PostWithCommentsSpecs.cs
```

### Application Layer

```
Blog.Application/
├── DTOs/
│   ├── Post/
│   ├── Comment/
│   ├── Category/
│   ├── BaseResponse.cs
│   ├── SingleResponse.cs
│   └── ListResponse.cs
├── Interfaces/
│   ├── IPostServices.cs
│   ├── ICommentService.cs
│   └── ICategoryService.cs
├── Services/
│   ├── PostService.cs
│   ├── CommentServices.cs
│   └── CategoryService.cs
└── Mapping/
    └── MappingProfile.cs
```

### Infrastructure Layer

```
Blog.Infrastructure/
├── Persistence/
│   ├── BlogDbContext.cs
│   └── AppDbContextDataSeeding.cs
├── Repositories/
│   ├── GenericRepository.cs
│   ├── CategoryRepository.cs
│   └── UnitOfWork.cs
└── ExternalService/
    └── AuthClient.cs
```

### API Layer

```
Blog.API/
├── Controllers/
│   ├── PostController.cs
│   ├── CommentController.cs
│   └── CategoryController.cs
├── Middlewares/
│   └── AuthMiddelware.cs
├── Extensions/
│   └── CustomAuthHandler.cs
└── Program.cs
```

## 🔑 Authentication

The API uses a custom authentication handler that integrates with an external authentication service. To access protected endpoints:

1. Obtain a bearer token from your authentication service
2. Include the token in the Authorization header:
   ```
   Authorization: Bearer {your-token}
   ```

## 🧪 Testing

Access the Swagger UI at `/swagger` to test all API endpoints interactively.

## 📦 NuGet Packages

Key dependencies include:

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Swashbuckle.AspNetCore`
- `Microsoft.AspNetCore.Authentication`


## 📝 Design Patterns Used

- **Repository Pattern**: Abstracts data access logic
- **Unit of Work Pattern**: Manages transactions across multiple repositories
- **Specification Pattern**: Encapsulates query logic
- **Dependency Injection**: Used throughout for loose coupling
- **DTO Pattern**: Separates domain models from API contracts

## 👤 Author

**Kamal Elsayed**

- GitHub: [@KamalElsayedJR](https://github.com/KamalElsayedJR)


## 🙏 Acknowledgments

- Built with Clean Architecture principles
- Inspired by Domain-Driven Design patterns
- Follows SOLID principles
