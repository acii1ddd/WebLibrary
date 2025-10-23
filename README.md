# WebLibrary

A RESTful API for managing a library of books and authors, built with ASP.NET Core 9.0.

## Architecture

The project follows a three-tier architecture:

- **WebLibrary.API** - REST API layer with controllers
- **WebLibrary.BLL** - Business logic layer with services
- **WebLibrary.DAL** - Data access layer with repositories
- **WebLibrary.API.Contracts** - Request/response DTOs

## Features

- CRUD operations for books (title, published year)
- CRUD operations for authors (name, date of birth)
- OpenAPI documentation with Scalar UI
- Custom exception handling

## API Endpoints

### Books
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Create new book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book

### Authors
- `GET /api/authors` - Get all authors
- `GET /api/authors/{id}` - Get author by ID
- `POST /api/authors` - Create new author
- `PUT /api/authors/{id}` - Update author
- `DELETE /api/authors/{id}` - Delete author

## Getting Started

### Prerequisites
- .NET 9.0 SDK

### Build & Run

```bash
dotnet build
dotnet run --project src/WebLibrary.API
```

### API Documentation

In development mode, access the interactive API docs at `/scalar/v1` after starting the application.

## Tech Stack

- ASP.NET Core 9.0
- Scalar (API documentation)
- Repository pattern
- Dependency injection
