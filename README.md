# Coupon Minimal API Project
<img width="4626" height="2906" alt="Image" src="https://github.com/user-attachments/assets/81ec8503-0827-4ca9-b1ca-6391fd861100" />
A modern, well-structured .NET 9 Minimal API project for managing coupons with authentication and authorization.

## 🚀 Features

- **Modern Architecture**: Clean separation of concerns with Repository, Service, and Endpoint layers
- **Organized Endpoints**: Each endpoint in its own class, organized by functionality (User, Coupons, Products, Orders)
- **JWT Authentication**: Secure authentication using JSON Web Tokens
- **Role-based Authorization**: Admin and Customer role support
- **Comprehensive Validation**: FluentValidation for request validation
- **AutoMapper**: Clean object mapping between models and DTOs
- **Entity Framework Core**: SQL Server database with migrations
- **Swagger Documentation**: Interactive API documentation with automatic schema generation
- **Proper Error Handling**: Consistent error responses with HTTP status codes
- **Nullable Reference Types**: Full nullable reference type support
- **Clean Code**: Simplified endpoint structure without verbose OpenAPI configurations
- **Clean Architecture**: Well-organized endpoint structure with proper separation of concerns

## 🏗️ Project Structure

```
Coupon-Minimal-API-Project/
├── Config/                 # Configuration classes
├── Data/                   # Database context and migrations
├── Endpoints/              # API endpoint definitions
│   ├── User/               # Authentication endpoints
│   └── Coupons/            # Coupon management endpoints
├── Migrations/             # Entity Framework migrations
├── Models/                 # Domain models and DTOs
│   ├── DTO/                # Data Transfer Objects
│   ├── Coupon.cs           # Coupon entity
│   ├── ApplicationUser.cs  # User entity
│   └── LocalUser.cs        # Local user entity
├── Repository/             # Data access layer
├── Services/               # Business logic layer
├── Validations/            # FluentValidation rules
├── Program.cs              # Application entry point
└── README.md               # This file
```

## 🛠️ Technologies

- **.NET 9** - Latest .NET framework
- **ASP.NET Core Minimal APIs** - Modern API development approach
- **Entity Framework Core 9** - Object-relational mapping
- **SQL Server** - Database
- **JWT Bearer** - Authentication
- **FluentValidation** - Request validation
- **AutoMapper** - Object mapping
- **Swagger/OpenAPI** - API documentation

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Coupon-Minimal-API-Project
   ```

2. **Update connection string**
   Edit `appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Coupon-Minimal-API-Project;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Update JWT Secret**
   Update the secret key in `appsettings.json`:
   ```json
   {
     "ApiSettings": {
       "Secret": "your-super-secret-key-here-minimum-32-characters"
     }
   }
   ```

4. **Run migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**
   Navigate to `https://localhost:7088/swagger`

## 📚 API Endpoints

### Authentication (`/api/auth`)
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/auth/check-username/{username}` - Check username availability

### Coupons (`/api/coupons`)
- `GET /api/coupons` - Get all coupons
- `GET /api/coupons/{id}` - Get coupon by ID
- `GET /api/coupons/name/{name}` - Get coupon by name
- `POST /api/coupons` - Create new coupon (Admin only)
- `PUT /api/coupons` - Update existing coupon (Admin only)
- `DELETE /api/coupons/{id}` - Delete coupon (Admin only)



### Health & Info
- `GET /` - API information
- `GET /health` - Health check

## 🔐 Authentication

The API uses JWT Bearer tokens for authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-jwt-token>
```

### Default Roles
- **Admin**: Full access to all endpoints
- **Customer**: Read-only access to coupons

## 📝 Request/Response Format

All API responses follow a consistent format:

```json
{
  "isSuccess": true,
  "result": { /* data */ },
  "statusCode": 200,
  "errorMessages": []
}
```

## 🧪 Testing

### Sample Requests

#### Register a new user
```bash
curl -X POST "https://localhost:7088/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "name": "Administrator",
    "password": "Admin123!"
  }'
```

#### Login
```bash
curl -X POST "https://localhost:7088/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "Admin123!"
  }'
```

#### Create a coupon (with JWT token)
```bash
curl -X POST "https://localhost:7088/api/coupons" \
  -H "Authorization: Bearer <your-jwt-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "SUMMER25",
    "percent": 25,
    "isActive": true
  }'
```



## 🔧 Configuration

### Identity Settings
- Password requirements: Minimum 6 characters, requires digit, lowercase, and uppercase
- Unique email requirement enabled
- JWT token expiration: 7 days

### Validation Rules
- Coupon names: 1-50 characters, alphanumeric only
- Discount percentage: 1-100 range
- Username: 3-50 characters, alphanumeric and underscores
- Password: Minimum 6 characters


## 🏗️ Architecture Features

### Clean Endpoint Organization
- **User Endpoints**: Authentication and user management operations
- **Coupon Endpoints**: Complete coupon CRUD operations with proper authorization
- **Tagged Organization**: All endpoints are properly tagged for better Swagger documentation

### Business Logic
- **Repository Pattern**: Clean data access layer abstraction
- **Service Layer**: Business logic separation with proper error handling
- **Validation**: Comprehensive request validation using FluentValidation
- **Authorization**: Role-based access control (Admin/Customer)

## 🚀 Deployment

### Docker
The project includes a Dockerfile for containerization:

```bash
docker build -t coupon-api .
docker run -p 8080:8080 -p 8081:8081 coupon-api
```

### Environment Variables
- `ASPNETCORE_ENVIRONMENT` - Set to "Production" for production
- `ConnectionStrings__DefaultConnection` - Database connection string
- `ApiSettings__Secret` - JWT secret key

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 🆘 Support

For support and questions, please open an issue in the repository.

## 📝 Recent Updates

### Endpoint Tagging (Latest)
- **User Endpoints**: All authentication endpoints now tagged with "User" for better Swagger organization
- **Coupon Endpoints**: All coupon management endpoints tagged with "Coupons" for clear categorization
- **Improved Documentation**: Better API organization in Swagger UI with logical grouping

### Endpoint Refactoring
- **Organized Structure**: Endpoints now organized by functionality in separate folders
- **Individual Classes**: Each endpoint has its own class for better maintainability
- **Cleaner Code**: Removed verbose OpenAPI configurations, letting Swagger auto-generate schemas
- **Better Namespaces**: Consistent namespace organization (`Endpoints.User`, `Endpoints.Coupons`, `Endpoints.Products`, `Endpoints.Orders`)
- **Centralized Mapping**: `EndpointsMapper.cs` handles all endpoint registration

### Benefits of the New Structure
- **Easier Maintenance**: Modify individual endpoints without affecting others
- **Better Testing**: Test each endpoint independently
- **Improved Readability**: Clear organization makes code easier to understand
- **Automatic Documentation**: Swagger generates schemas automatically from DTOs
- **Scalability**: Easy to add new endpoints following the established pattern
- **Better Organization**: Clean, focused project structure for coupon management
