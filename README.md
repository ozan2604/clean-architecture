# 2025 Clean Architecture Setup

This repository provides a modern and modular Clean Architecture structure that you can use as a starting point for your 2025 projects.



## 📁 Project Content

### Architecture

- **Architectural Pattern**: Clean Architecture

### Design Patterns Used

- Result Pattern  
- Repository Pattern  
- CQRS Pattern  
- UnitOfWork Pattern

### 📦 Libraries & Technologies

- **MediatR**: For CQRS and messaging operations  
- **TS.Result**: For standard result modeling  
- **Mapster**: For object mapping  
- **FluentValidation**: For validation operations  
- **TS.EntityFrameworkCore.GenericRepository**: Generic repository implementation  
- **EntityFrameworkCore**: ORM for database operations  
- **OData**: For flexible querying and data shaping  
- **Scrutor**: For DI management and dynamic service registration  
- **Microsoft.AspNetCore.Authentication.JwtBearer**: For JWT-based authentication  
- **Keycloak.AuthServices.Authentication**: For Keycloak-based authentication

## ⚙️ Setup & Usage

### 1. Clone the Repository

```bash
git clone https://github.com/ozan2604/clean-architecture.git
cd clean-architecture