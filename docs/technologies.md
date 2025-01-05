# Technologies Documentation 📚

## Overview

This document outlines the technologies, frameworks, and libraries used in the WiseReminder project

---

## Core Technologies

### 1. **Programming Language and Framework**

- **C#**: The project is built using the C# programming language
- **.NET 9.0**: Utilized ASP.NET Core for building the Web API, ensuring high performance and scalability

### 2. **Architectural Patterns**

- **DDD (Domain-Driven Design)**: Applied to encapsulate business logic within a dedicated domain layer
- **Clean Architecture**: Implemented to separate concerns into four distinct layers:
    - **Domain**: Encapsulates business logic
    - **Application**: Handles application-specific concerns like CQRS
    - **Infrastructure**: Manages data access, caching, and external services
    - **WebAPI**: Provides the entry point for client interactions

---

## Layers and Libraries

### **1. Domain Layer**

- **Purpose**: Contains the business logic and domain entities
- **Key Library**: [FluentResults](https://github.com/altmann/FluentResults) for implementing the Result Pattern
    - Provides a structured way to return success or failure states with detailed messages
    - Used in factory methods for creating domain entities while ensuring business rules are respected

---

### **2. Application Layer**

- **Purpose**: Implements application logic and handles CQRS (Command Query Responsibility Segregation)
- **Key Libraries**:
    - [MediatR](https://github.com/jbogard/MediatR): Manages CQRS by separating commands and queries
    - [Serilog](https://serilog.net/): Logs application events to the console and Seq for enhanced debugging and analysis
        - **Seq**: Used as a centralized log management tool
    - Unit of Work Pattern: Abstracted as an interface and implemented in the Infrastructure layer

---

### **3. Infrastructure Layer**

- **Purpose**: Handles database access, caching, and external integrations
- **Key Technologies**:
    - **Entity Framework Core (EF Core)**:
        - Manages database operations and migrations
        - PostgreSQL used as the database
    - **BCrypt**:
        - Encrypts user passwords for secure storage
    - **JWT Authentication**:
        - Manages user authentication with token-based mechanisms
    - **Redis**:
        - Provides caching for improved performance and scalability
    - **Data Seeding**:
        - Preloads initial data into the database during application startup

---

### **4. WebAPI Layer**

- **Purpose**: Serves as the entry point for client requests and integrates all layers
- **Key Libraries**:
    - [FluentValidation](https://fluentvalidation.net/): Validates incoming requests to ensure data integrity
    - **JWT Support**:
        - Used for securing API endpoints and authorizing user requests
    - [Scalar](https://scalar.github.io/): Alternative to Swagger UI for API documentation
    - Custom Mapping Classes:
        - Maps DTOs to domain models and vice versa

---

## Containers and Deployment

- **Docker**:
    - Dockerized the Web API using a Dockerfile, exposing the application on port 8080
    - Created a `docker-compose` file to orchestrate:
        - **PostgreSQL**
        - **Redis**
        - **Seq**
    - Ensures that the latest versions of images are pulled and configured with appropriate connection strings

---

## Testing

### **1. Testing Frameworks**

- **xUnit**: Framework for writing unit and integration tests
- **FluentAssertions**: Simplifies assertions in tests with a fluent syntax
- **Coverlet.Collector**: Measures code coverage for tests

### **2. Test Projects**

- **WiseReminder.ArchitectureTests**:
    - Validates adherence to architectural rules
- **WiseReminder.Domain.UnitTests**:
    - Tests business logic within the domain layer
- **WiseReminder.WebAPI.IntegrationTests**:
    - Verifies API methods with all possible scenarios

### **3. Integration Testing Tools**

- **TestContainers**:
    - Creates lightweight containers (using `alpine` images) for PostgreSQL and Redis during integration tests

---

## Summary

The **WiseReminder** project combines modern technologies, robust architectural patterns, and comprehensive testing to ensure scalability, maintainability, and a seamless developer experience. Its use of Docker containers and advanced logging with Seq enhances its deployability and observability

