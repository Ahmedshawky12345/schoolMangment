# School Management API

A comprehensive school management system API built with ASP.NET Core. This project manages users, roles, departments, classes, students, teachers, courses, and exams with full CRUD operations and additional functionalities. 

## Features

- **Authentication & Authorization**: Secure login and registration for users with role-based access control. Supported roles include:
  - **Admin**
  - **Student**
  - **Teacher**
- **Models & CRUD Operations**: Manage various entities including:
  - **Department**: Manages departments in the school.
  - **Class**: Organizes students into classes.
  - **Student**: Manages student information and enrollment.
  - **Teacher**: Manages teacher profiles and assignments.
  - **Course**: Defines courses taught in the school.
  - **Exam**: Manages exams and assessments.
- **Advanced Operations**:
  - Retrieve students in a specific class or department.
  - Retrieve grades for a specific student in a particular class.
- **Dependency Injection**: Implements dependency injection to manage services.
- **Repository & Unit of Work Patterns**: Ensures clean architecture and maintainable code by implementing these design patterns.
- **AutoMapper**: Simplifies object mapping between models and DTOs.
- **Data Transfer Objects (DTOs)**: Uses DTOs to control data exposure and optimize responses.
- **JWT Authentication**: Generates JWT tokens for secure API authentication and role-based access.
- **Swagger & Postman Testing**: Provides API documentation with Swagger, and uses Postman for testing API endpoints.
- **SQL Server**: SQL Server is used to store all application data.

## Technologies Used

- **ASP.NET Core**: Backend framework for building the API.
- **Entity Framework Core**: ORM for database access and management.
- **SQL Server**: Database to store API data.
- **JWT**: For secure authentication and token-based authorization.
- **AutoMapper**: For mapping models to DTOs.
- **Swagger**: For API documentation and testing.
- **Postman**: For manual API testing.


