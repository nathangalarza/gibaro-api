# My .NET Core API Project - GIbaro API

## Overview
This API is built using .NET Core and follows a clean architecture with a focus on the repository pattern, service layer, and Entity Framework Core for database interactions. It's designed to provide a scalable and maintainable codebase.

## Technologies Used
- .NET Core
- Entity Framework Core
- Repository Pattern
- Service Layer

## Getting Started

### Prerequisites
- .NET Core SDK (version 3.0 or later)
- A supported database engine (e.g., SQL Server, PostgreSQL)

### Installation
1. Clone the repository
   ```bash
   git clone https://github.com/nathangalarza/gibaro-api.git
## Architecture

### Repository Pattern
The repository pattern is used to separate the logic that retrieves data from the database from the business logic. This abstraction makes the code more maintainable and testable.

### Service Layer
The service layer encapsulates the business logic and provides a clear API to the controllers. It interacts with the repositories to retrieve data, apply business rules, and handle transactions.

### Entity Framework Core
Entity Framework Core is used for database interactions, enabling smooth CRUD operations with the database using LINQ queries.

## API Documentation
Detailed API documentation is available at /swagger.

## Contact
Nathan J. Galarza - nathanjavier0917@gmail.com
LinkedIn: https://www.linkedin.com/in/nathan-galarza-916470149/


