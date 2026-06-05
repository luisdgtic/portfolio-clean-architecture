# Portfolio

[![CI](https://github.com/luisantonio/portfolio/actions/workflows/ci.yml/badge.svg)](https://github.com/luisantonio/portfolio/actions/workflows/ci.yml)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![React 19](https://img.shields.io/badge/React-19-61DAFB?logo=react)](https://react.dev/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.8-3178C6?logo=typescript)](https://www.typescriptlang.org/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3-38B2AC?logo=tailwindcss)](https://tailwindcss.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)](https://www.docker.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Professional full-stack portfolio web application built with **.NET 9 Clean Architecture** and **React SPA**. Designed to showcase software engineering skills to technical recruiters.

## Architecture

```
┌─────────────────────────────────────────────────┐
│                    Client (React SPA)            │
│         Tailwind CSS · TanStack Query · Axios   │
└─────────────────┬───────────────────────────────┘
                  │ HTTP REST
┌─────────────────▼───────────────────────────────┐
│              Web API (.NET 9)                    │
│      Controllers · Middleware · Serilog          │
└─────────────────┬───────────────────────────────┘
                  │ MediatR · CQRS
┌─────────────────▼───────────────────────────────┐
│           Application Layer                      │
│    Queries/Commands · Validators · Behaviors     │
└─────────────────┬───────────────────────────────┘
                  │ Repository Interfaces
┌─────────────────▼───────────────────────────────┐
│          Infrastructure Layer                    │
│    EF Core · PostgreSQL/SQLite · Repositories    │
└─────────────────┬───────────────────────────────┘
                  │
┌─────────────────▼───────────────────────────────┐
│              Domain Layer                        │
│      Entities · Value Objects · Enums            │
└─────────────────────────────────────────────────┘
```

### Solution Structure

```
Portfolio/
├── src/
│   ├── Portfolio.Domain/          # Entities, Value Objects, Interfaces
│   ├── Portfolio.Application/     # CQRS (MediatR), FluentValidation, Behaviors
│   ├── Portfolio.Infrastructure/  # EF Core, Repositories, Seed Data, Health Checks
│   └── Portfolio.WebApi/          # Controllers, Middleware, Program
├── tests/
│   ├── Portfolio.Domain.Tests/
│   └── Portfolio.Application.Tests/   # 30 tests, 100% pass rate
└── client/                        # React 19 + TypeScript + Tailwind CSS
```

## Tech Stack

### Backend
- **.NET 9** with ASP.NET Core (Controller-based API)
- **Clean Architecture** with CQRS pattern
- **MediatR** for request/response pipeline
- **FluentValidation** for input validation
- **Entity Framework Core 9** with PostgreSQL (production) and SQLite (development)
- **Serilog** for structured logging
- **ProblemDetails (RFC 7807)** for error responses
- **Health Checks** for monitoring

### Frontend
- **React 19** with TypeScript 5.8
- **Vite 6** for build tooling
- **Tailwind CSS 3** for styling
- **TanStack Query (React Query)** for server state
- **React Router 7** with lazy loading
- **Axios** for HTTP client with interceptors
- **Dark mode** with localStorage persistence

### DevOps
- **Docker** multi-stage builds
- **Docker Compose** for local development
- **Render** for cloud hosting (free tier)
- **GitHub Actions** for CI/CD
- **PostgreSQL 16** managed database

## Quick Start

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (optional)

### Local Development

**Backend (SQLite):**
```bash
cd src/Portfolio.WebApi
dotnet run
# API available at http://localhost:5000
# Health check: http://localhost:5000/api/health
```

**Frontend:**
```bash
cd client
npm install
npm run dev
# Available at http://localhost:5173
```

**Testing:**
```bash
dotnet test
# Output: 30 passed, 0 failed, 0 skipped
```

### Docker (Full Stack)
```bash
docker compose up -d
# API:     http://localhost:5000
# Client:  http://localhost:3000
```

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `UseSqlite` | Use SQLite instead of PostgreSQL | `true` (dev) |
| `ConnectionStrings__Postgres` | PostgreSQL connection string | - |
| `ConnectionStrings__Sqlite` | SQLite connection string | `Data Source=portfolio_dev.db` |
| `CorsOrigins__0` | Allowed CORS origin | `http://localhost:5173` |

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/health` | Health check with database status |
| `GET` | `/api/profile` | Portfolio owner profile |
| `GET` | `/api/projects` | All projects (`?featured=true` to filter) |
| `GET` | `/api/projects/{id}` | Project detail by GUID |
| `GET` | `/api/skills` | All skills (`?category=Backend` to filter) |
| `GET` | `/api/experiences` | Work experience (timeline) |
| `GET` | `/api/education` | Education history |
| `GET` | `/api/certifications` | Certifications |
| `GET` | `/api/blog` | Blog posts (`?page=1&pageSize=10`) |
| `GET` | `/api/blog/{slug}` | Blog post by slug |
| `POST` | `/api/contact` | Submit contact message |

## Testing

- **30 unit tests** in Application layer
- **0 failures**, 100% pass rate
- Framework: **xUnit + Moq + FluentAssertions**
- Covers: handlers, validators, behaviors, edge cases

## Deployment

This project is configured for **Render** with a Blueprint spec (`render.yaml`):
- **Web Service**: .NET API (Docker)
- **Static Site**: React SPA (Docker + Nginx)
- **PostgreSQL**: Managed database (free tier)

CI/CD is handled by **GitHub Actions** on every push to `main`.

## License

MIT © [Luis Antonio](https://github.com/luisdgtic)
