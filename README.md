# CursosDev - Course Management Platform

A Full Stack application for managing courses and lessons, built with **.NET 10 (Hexagonal Architecture)** and **Angular 21**.

## 🚀 Getting Started

### Prerequisites

- [Docker & Docker Compose](https://www.docker.com/) (Recommended)
- **Optional (for local dev):**
  - .NET 10 SDK
  - Node.js 22+ & Angular CLI 21
  - PostgreSQL 17

---

### 🐳 Quick Start (Docker)

The easiest way to run the application is using Docker Compose. Authentication credentials, database configuration, and networking are already set up.

1.  **Clone the repository:**

    ```bash
    git clone <repository-url>
    cd CursosDev
    ```

2.  **Start the specific services (Test -> API -> Web):**

    ```bash
    docker-compose up --build
    ```

    _This command will first run unit tests. If they pass, the API and Frontend will start._

3.  **Access the application:**
    - **Frontend:** [http://localhost:4200](http://localhost:4200)
    - **API Swagger:** [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

### 🗄️ Database Configuration & Migrations

The project uses **PostgreSQL**.

#### Via Docker (Automatic)

The DB is configured in `docker-compose.yml` to run on port `5432`.
**Migrations are automatically applied** when the API container starts. You do not need to run manual commands.

#### Manual Configuration (Local Dev)

If running outside Docker:

1. Update `ConnectionStrings:DefaultConnection` in `appsettings.json`.
2. Run migrations:
   ```bash
   cd CursosDev.Api
   dotnet ef database update
   ```

---

### 👤 Test User Credentials

A default test user is **automatically created** when the API starts.

**Credentials:**

- **Email:** `test@cursosdev.com`
- **Password:** `Test123!`

You can also register new users at: [http://localhost:4200/auth/register](http://localhost:4200/auth/register).

---

## 🛠️ Project Structure & Architecture

### Backend (CursosDev.Api)

Built following **Hexagonal Architecture (Ports and Adapters)** principles.

- **Domain:** Entities and Repository Interfaces (Ports).
- **Application:** Business Logic (Use Cases).
- **Infrastructure:** Implementation of Repositories, Database Context (EF Core), and services.
- **Api:** Controllers and entry point.
- **Tests:** Unit tests with xUnit and Moq.

### Frontend (CursosDev.Web)

Built with **Angular 21** and **Tailwind CSS**.

- **Core:** Singleton services and guards.
- **Features:** Modularized features (Auth, Courses, Lessons).
- **Shared:** Reusable components.
- **Refactoring:** Validated use of `inject()` and separate HTML templates.

## 🧪 Running Tests

To run the backend unit tests manually:

**Via Docker:**

```bash
docker-compose up cursos-tests
```

**Via CLI:**

```bash
cd CursosDev.Tests
dotnet test
```

## 📝 Technologies

- **.NET 10**
- **Angular 21**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker & Docker Compose**
- **xUnit**
- **Tailwind CSS 3**
