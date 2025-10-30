# WebLibrary

A RESTful API for managing a library of books and authors, built with ASP.NET Core 9.0 and SQL Server.

## Architecture

The project follows a clean three-tier architecture:

- **WebLibrary.API** - REST API layer with controllers and exception handlers
- **WebLibrary.BLL** - Business logic layer with services and custom exceptions
- **WebLibrary.DAL** - Data access layer with Entity Framework Core, repositories, and migrations
- **WebLibrary.API.Contracts** - Request/response contracts

## Project Structure

```
WebLibrary/
├── src/
│   ├── WebLibrary.API/          # API controllers, exception handlers, Program.cs
│   ├── WebLibrary.BLL/          # Services, interfaces, business exceptions
│   ├── WebLibrary.DAL/          # EF Core context, repositories, models, migrations
│   └── WebLibrary.API.Contracts/ # Request/response DTOs
├── docker/                       # Docker Compose configuration
└── README.md
```

## Features

- **Books Management**
  - CRUD operations for books (title, published year, author)
  - Filter books by publication year
  - Relationship management with authors

- **Authors Management**
  - CRUD operations for authors (name, date of birth)
  - Filter authors by name
  - Get authors with book count statistics
  - One-to-many relationship with books

- **Technical Features**
  - Entity Framework Core with SQL Server
  - Repository pattern implementation
  - Automatic database migrations in development
  - Data seeding with initial sample data
  - OpenAPI documentation with Scalar UI
  - Global exception handling middleware
  - Dependency injection throughout
  - Docker Compose support for SQL Server

## Data Model

### Author
- `Id` (Guid)
- `Name` (string)
- `DateOfBirth` (DateTime)
- `Books` (collection)

### Book
- `Id` (Guid)
- `Title` (string)
- `PublishedYear` (int)
- `AuthorId` (Guid)
- `Author` (navigation property)

## API Endpoints

### Books
- `GET /api/books` - Get all books (optional filter: `?startYear={year}`)
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Create new book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book

### Authors
- `GET /api/authors` - Get all authors (optional filter: `?name={name}`)
- `GET /api/authors/{id}` - Get author by ID
- `GET /api/authors/books-count` - Get authors with their book counts
- `POST /api/authors` - Create new author
- `PUT /api/authors/{id}` - Update author
- `DELETE /api/authors/{id}` - Delete author

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Docker and Docker Compose (for SQL Server)

### Database Setup

1. Start SQL Server using Docker Compose:
```bash
cd docker
docker-compose up -d --build
```

2. Update connection string in `src/WebLibrary.API/appsettings.Development.json` if needed.

### Build & Run

```bash
cd ../src/WebLibrary.API
dotnet build
dotnet run --project src/WebLibrary.API
```

In development mode, migrations are applied automatically and sample data is seeded on startup.

### API Documentation

Once the application is running, access the interactive API documentation:
- **Scalar UI**: `http://localhost:<port>/scalar/v1`
- **OpenAPI JSON**: `http://localhost:<port>/openapi/v1.json`

## Tech Stack

- **ASP.NET Core 9.0** - Web framework
- **Entity Framework Core** - ORM
- **SQL Server 2025** - Database
- **Scalar** - API documentation UI
- **Docker** - Containerization
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Service management
