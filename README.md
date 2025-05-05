# Employee Management REST API

A robust REST API for managing employee records, built with ASP.NET Core following clean architecture and SOLID principles.

## Features

- ✅ Create new employees
- ✅ Delete existing employees
- ✅ Retrieve all employees
- ✅ Email uniqueness validation
- ✅ RESTful architecture
- ✅ Comprehensive test suite

## Technology Stack

- ASP.NET Core 8.0
- xUnit for testing
- Moq for mocking in tests

## Architecture

The project follows clean architecture principles with clear separation of concerns:

```
PersonnelApi/
├── Controllers/         # API endpoints
├── Models/             # Domain entities
├── DTOs/               # Data transfer objects
├── Repositories/       # Data access layer
└── Tests/             # Unit tests

```

### Design Patterns Used

- **Repository Pattern**: Abstracts data persistence concerns
- **Dependency Injection**: Used throughout for loose coupling
- **DTO Pattern**: Separates domain models from API contracts
- **Unit of Work**: Manages data operations and transactions

## API Endpoints

### Get All Employees
```http
GET /api/employees
```
Returns a list of all employees.

**Response**: 200 OK
```json
[
  {
    "id": "guid",
    "firstName": "string",
    "lastName": "string",
    "email": "string"
  }
]
```

### Create Employee
```http
POST /api/employees
```
Creates a new employee.

**Request Body**:
```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string"
}
```

**Responses**:
- 201 Created: Employee successfully created
- 409 Conflict: Email already exists

### Delete Employee
```http
DELETE /api/employees/{id}
```
Deletes an employee by ID.

**Responses**:
- 204 No Content: Successfully deleted
- 404 Not Found: Employee not found

## Testing

The project includes a comprehensive test suite covering both the repository and controller layers. Tests are written using xUnit and follow the Arrange-Act-Assert pattern.

To run the tests:
```bash
dotnet test
```

### Test Coverage

- **Repository Tests**:
  - Empty repository behavior
  - Employee creation and retrieval
  - Email uniqueness validation
  - Deletion scenarios
  - Edge cases

- **Controller Tests**:
  - Successful operations
  - Error cases
  - Input validation
  - HTTP status codes
  - Response formats

## Getting Started

1. Clone the repository
```bash
git clone https://github.com/Denizhan907/employee-rest-api.git
```

2. Navigate to the project directory
```bash
cd employee-rest-api
```

3. Build the project
```bash
dotnet build
```

4. Run the application
```bash
dotnet run --project PersonnelApi
```

## Best Practices Implemented

- ✅ SOLID Principles
- ✅ Clean Architecture
- ✅ Dependency Injection
- ✅ Input Validation
- ✅ Error Handling
- ✅ Unit Testing
- ✅ API Documentation
- ✅ Consistent Code Style

## Error Handling

The API implements consistent error handling with appropriate HTTP status codes:

- 200: Successful GET requests
- 201: Successful resource creation
- 204: Successful deletion
- 400: Bad request / Invalid input
- 404: Resource not found
- 409: Conflict (e.g., duplicate email)
- 500: Server error

## Future Enhancements

- [ ] Add authentication and authorization
- [ ] Implement pagination for GET endpoints
- [ ] Add more advanced filtering and sorting
- [ ] Implement logging and monitoring
- [ ] Add OpenAPI/Swagger documentation
- [ ] Add data persistence with Entity Framework 