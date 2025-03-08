# CinemaApp Microservices Architecture 🏗️

## Folder Structure Overview 🗂️

### Root Level

- `.gitignore` - Git ignore file to exclude unnecessary files from version control.
- `.dockerignore` - Docker ignore file to exclude unnecessary files from Docker builds.
- `docker-compose.yml` - Docker Compose configuration for the microservices application.
- `CinemaMicroservice.sln` - Solution file for the CinemaMicroservice project.

### /Services – Microservices

- **📅 /CinemaApp.API** – Presentation Layer (API Controllers)
  - **/Controllers** – HTTP API controllers
    - `StudioController.cs` – Handles studio-related API requests.
    - `MovieController.cs` – Manages movie-related API requests.
    - `TicketController.cs` – Manages ticket-related API requests.
    - `ScheduleController.cs` – Manages schedule-related API requests.
  - **/Models** – Data Transfer Objects (DTOs) for API communication
    - `StudioDto.cs` – DTO for studio data.
    - `MovieDto.cs` – DTO for movie data.
    - `TicketDto.cs` – DTO for ticket data.

- **🎬 /CinemaApp.Application** – Application Layer (Use Case / Services)
  - **/Interfaces** – Interfaces for application services
    - `IStudioService.cs` – Interface for studio service.
    - `IMovieService.cs` – Interface for movie service.
    - `ITicketingService.cs` – Interface for ticketing service.
  - **/Services** – Business logic / Service Implementations
    - `StudioService.cs` – Service for managing studios.
    - `MovieService.cs` – Service for managing movies.
    - `TicketingService.cs` – Service for handling ticket bookings.

- **🎟️ /CinemaApp.Domain** – Domain Layer (Entities and Core Logic)
  - **/Entities** – Core entities of the application
    - `Studio.cs` – Entity representing a movie studio.
    - `Movie.cs` – Entity representing a movie.
    - `Ticket.cs` – Entity representing a ticket.
  - **/Interfaces** – Interfaces for repositories or domain logic
    - `IStudioRepository.cs` – Interface for studio repository.
    - `IMovieRepository.cs` – Interface for movie repository.
    - `ITicketRepository.cs` – Interface for ticket repository.

- **💳 /CinemaApp.Infrastructure** – Infrastructure Layer (External dependencies and database)
  - **/Repositories** – Repository Implementations
    - `StudioRepository.cs` – Repository for accessing studio data.
    - `MovieRepository.cs` – Repository for accessing movie data.
    - `TicketRepository.cs` – Repository for accessing ticket data.
  - **/Services** – External services and integrations
    - `NotificationService.cs` – Service for sending notifications.
    - `ExternalPaymentService.cs` – Service for handling external payments.

### /Shared – Shared Components for All Microservices

- **/Shared.Infrastructure** – Shared infrastructure services (e.g., logging, messaging)
- **/Shared.Domain** – Shared domain logic and entities
- **/Shared.Messaging** – Shared messaging logic (e.g., queues, events)
- **/Shared.Logging** – Shared logging utilities

### /Configs – Configuration Files

- **/ServiceConfigurations** – Configuration for service settings.
- **/DatabaseMigrations** – Database migration scripts.

### /Deployments – Deployment Files

- **docker-compose.yml** – Deployment configuration for the microservices application.

### /Tests – Unit and Integration Tests

- **/UnitTests** – Unit tests for each microservice
  - `MovieServiceTests.cs` – Unit tests for the movie service.
  - `TicketServiceTests.cs` – Unit tests for the ticketing service.
- **/IntegrationTests** – Integration tests for the entire application
  - `ApiIntegrationTests.cs` – API integration tests.

### /Docs – Documentation Files

- `README.md` – Project overview and setup instructions.

---

## Tech Stack 🛠️

- **🖥️ Framework**: .NET 8 (C#)
- **🗄️ Database**: Microsoft SQL Server 2022
- **🐳 Deployment**: Docker & Docker Compose
- **🔐 Authentication**: JWT & OAuth
- **🔀 API Gateway**: Ocelot (if needed for routing)
