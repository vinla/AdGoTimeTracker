# AdGo Time Tracker

A full-stack time tracking application built with Blazor WebAssembly, ASP.NET Core Web API, and SQL Server.

## Project Overview

AdGo Time Tracker is a web-based time tracking solution that allows users to track their time entries. The application features a Blazor WebAssembly frontend with a clean UI powered by MudBlazor, a RESTful API backend, and flexible data storage options (in-memory or SQL Server).

## Project Structure

```
AdGoTimeTracker/
├── AdGoTimeTracker.sln                  # Solution file
├── docker-compose.yml                    # Docker Compose configuration
├── api.Dockerfile                        # API Docker image definition
├── blazor.dockerFile                     # Blazor app Docker image definition
│
├── AdGoTimeTracker/                      # Blazor Server Host Project
│   ├── Program.cs                        # Blazor host startup
│   ├── Components/                       # Blazor components
│   └── appsettings.json                  # Configuration
│
├── AdGoTimeTracker.Client/               # Blazor WebAssembly Client
│   ├── Components/                       # UI components
│   │   ├── StopwatchButton.razor
│   │   ├── TimeEntryForm.razor
│   │   └── TimeTrackerList.razor
│   ├── Pages/                            # Page components
│   │   └── Home.razor
│   ├── Services/                         # Client services
│   │   └── TimeTrackerService.cs
│   ├── Models/                           # Client models
│   └── Validators/                       # Form validators
│
├── AdGoTimeTracker.Api/                  # ASP.NET Core Web API
│   ├── Program.cs                        # API startup and configuration
│   ├── Controllers/                      # API controllers
│   │   └── EntryController.cs
│   ├── IUserContext.cs                   # User context interface
│   └── appsettings.json                  # API configuration
│
├── AdGoTimeTracker.Core/                 # Core Domain Layer
│   ├── Interfaces/                       # Core interfaces
│   └── Models/                           # Domain models
│
├── AdGoTimeTracker.MemoryStore/          # In-Memory Data Store
│   └── InMemoryTimeTrackerEntryStore.cs
│
├── AdGoTimeTracker.SqlEntityStore/       # SQL Server Data Store
│   ├── ApplicationDbContext.cs           # Entity Framework DbContext
│   ├── SqlEntityTimeTrackerEntryStore.cs # SQL implementation
│   └── TimeTrackerEntryEntity.cs         # Database entity
│
├── AdGoTimeTracker.Client.UnitTests/     # Unit Tests
│   └── TimeTrackerEntryFormDataValidatorTests.cs
│
└── sql/                                  # Database Scripts
    └── Scheme_01.sql
```

## Architecture

### Frontend
- **Framework**: Blazor WebAssembly with .NET 8+
- **UI Library**: MudBlazor
- **Hosting**: Blazor Server with WebAssembly prerendering

### Backend
- **Framework**: ASP.NET Core Web API
- **Data Storage**: Configurable (In-Memory or SQL Server)
- **CORS**: Enabled for cross-origin requests

### Data Layer
- **Core**: Domain models and interfaces
- **MemoryStore**: In-memory implementation for development/testing
- **SqlEntityStore**: Entity Framework Core with SQL Server

## Running with Visual Studio

### Prerequisites
- Visual Studio 2022 or later
- .NET 8.0 SDK or later
- (Optional) SQL Server for persistent storage

### Steps

1. **Open the Solution**
   - Open `AdGoTimeTracker.sln` in Visual Studio

2. **Configure Multiple Startup Projects**
   - Right-click on the solution in Solution Explorer
   - Select **Properties**
   - Go to **Startup Project**
   - Select **Multiple startup projects**
   - Set both `AdGoTimeTracker` and `AdGoTimeTracker.Api` to **Start**
   - Click **OK**

3. **Configure the API Storage (Optional)**
   - Open `AdGoTimeTracker.Api/appsettings.json`
   - By default, it uses in-memory storage (`"STORE_TYPE": "in-memory"`)
   - To use SQL Server, change to:
     ```json
     {
       "STORE_TYPE": "mssql",
       "CONNECTION_STRING": "Server=localhost;Database=AdGo;Trusted_Connection=True;TrustServerCertificate=True"
     }
     ```

4. **Configure the Blazor App API URL (if needed)**
   - Open `AdGoTimeTracker/appsettings.json`
   - Default API URL is `http://localhost:5007`
   - Modify if your API runs on a different port

5. **Run the Application**
   - Press **F5** or click **Start**
   - The API will start on `http://localhost:5007`
   - The Blazor app will start on `http://localhost:5010`
   - Your browser will open automatically to the Blazor app

### Development URLs
- **Blazor App**: http://localhost:5010 (HTTP) or https://localhost:7215 (HTTPS)
- **API**: http://localhost:5007 (HTTP) or https://localhost:7280 (HTTPS)

## Running with Docker Compose

### Prerequisites
- Docker Desktop installed and running
- Docker Compose (included with Docker Desktop)

### Steps

1. **Build and Start All Services**
   ```bash
   docker-compose up --build
   ```

   Or to run in detached mode:
   ```bash
   docker-compose up --build -d
   ```

2. **Access the Application**
   - **Blazor App**: http://localhost:3000
   - **API**: http://localhost:5000
   - **SQL Server**: localhost:1433

3. **Stop the Services**
   ```bash
   docker-compose down
   ```

### Docker Compose Services

The `docker-compose.yml` configures three services:

1. **blazor** (Frontend)
   - Built from `blazor.dockerFile`
   - Exposed on port 3000
   - Image: `timetracker-blazor`

2. **api** (Backend)
   - Built from `api.Dockerfile`
   - Exposed on port 5000
   - Configured to use SQL Server storage
   - Image: `timetracker-api`
   - Environment Variables:
     - `STORE_TYPE=mssql`
     - `CONNECTION_STRING=Server=db;Database=AdGo;...`

3. **db** (Database)
   - Image: Microsoft SQL Server 2022
   - Exposed on port 1433
   - Credentials:
     - SA Password: `password123!`
   - Initialization scripts from `./init` directory

### Building Individual Images

To build just the API:
```bash
docker build -f api.Dockerfile -t timetracker-api .
```

To build just the Blazor app:
```bash
docker build -f blazor.dockerFile -t timetracker-blazor .
```

## Configuration

### API Configuration Options

The API can be configured via environment variables or `appsettings.json`:

- **STORE_TYPE**: 
  - `in-memory` - Uses in-memory storage (default for development)
  - `mssql` - Uses SQL Server storage

- **CONNECTION_STRING**: SQL Server connection string (required when `STORE_TYPE=mssql`)

### Blazor App Configuration

- **ApiUrl**: The base URL of the API backend (default: `http://localhost:5007`)

## Features

- ⏱️ Time entry tracking with start/stop functionality
- 📝 Create, view, and manage time entries
- 🎨 Modern UI with MudBlazor components
- 💾 Flexible storage: In-memory or SQL Server
- 🐳 Docker containerization support
- ✅ Unit tests included

## Development

### Running Tests

In Visual Studio:
- Open **Test Explorer** (Test > Test Explorer)
- Click **Run All** to execute all unit tests

Or via command line:
```bash
dotnet test
```

## Technologies Used

- .NET 8+
- Blazor WebAssembly
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MudBlazor
- Docker & Docker Compose
