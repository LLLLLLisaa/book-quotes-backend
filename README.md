# Book Quotes Backend

Book Quotes Backend is an ASP.NET Core Web API that provides the backend services for the Book Quotes application.

## Tech Stack
- ASP.NET Core
- C#
- .NET 10
- Entity Framework Core
- SQLite
- JWT Authentication

## Features
- User registration and login
- JWT authentication
- Book CRUD
- Quote CRUD
- User-specific data access
- Swagger API documentation

## Run
```bash
dotnet run
```

## Swagger
After starting the application, open the Swagger URL displayed in the terminal.

## Database
- SQLite

## Live Demo

Backend API:
https://book-quotes-backend-production.up.railway.app

Swagger UI:
https://book-quotes-backend-production.up.railway.app/swagger

The root URL redirects directly to Swagger UI.

## Deployment Note

The online demo uses SQLite without persistent storage. Data created in the deployed environment may be reset if the Railway container is recreated or restarted.