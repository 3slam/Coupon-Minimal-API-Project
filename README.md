# Coupon Minimal API Project

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
- **E-commerce Features**: Complete product catalog and order management system
- **Inventory Management**: Stock tracking and management
- **Order Processing**: Full order lifecycle management
- **Category Management**: Hierarchical product categorization

## 🏗️ Project Structure

```
Coupon-Minimal-API-Project/
├── Config/                 # Configuration classes
├── Data/                   # Database context and migrations
├── Endpoints/              # API endpoint definitions
│   ├── User/               # Authentication endpoints
│   ├── Coupons/            # Coupon management endpoints
│   ├── Products/           # Product management endpoints
│   └── Orders/             # Order management endpoints
├── Migrations/             # Entity Framework migrations
├── Models/                 # Domain models and DTOs
│   ├── DTO/                # Data Transfer Objects
│   ├── Product.cs          # Product entity
│   ├── Category.cs         # Category entity
│   ├── Order.cs            # Order entity
│   └── OrderItem.cs        # Order item entity
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

### Products (`/api/products`)
- `GET /api/products` - Get all products
- `POST /api/products` - Create new product (Admin only)
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/sku/{sku}` - Get product by SKU
- `GET /api/products/category/{category}` - Get products by category
- `GET /api/products/brand/{brand}` - Get products by brand
- `GET /api/products/search/{term}` - Search products
- `PUT /api/products` - Update product (Admin only)
- `DELETE /api/products/{id}` - Delete product (Admin only)

### Orders (`/api/orders`)
- `POST /api/orders` - Create new order (Authenticated users)
- `GET /api/orders` - Get user orders (Authenticated users)
- `GET /api/orders/{id}` - Get order by ID
- `GET /api/orders/status/{status}` - Get orders by status (Admin only)
- `PUT /api/orders/{id}/status` - Update order status (Admin only)
- `DELETE /api/orders/{id}` - Delete order (Admin only)

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

#### Create a product (Admin only)
```bash
curl -X POST "https://localhost:7088/api/products" \
  -H "Authorization: Bearer <your-jwt-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Gaming Laptop",
    "description": "High-performance gaming laptop with RTX graphics",
    "price": 1299.99,
    "stockQuantity": 25,
    "category": "Electronics",
    "brand": "GameTech",
    "isActive": true,
    "sku": "LAPTOP-GAMING-001"
  }'
```

#### Create an order (Authenticated users)
```bash
curl -X POST "https://localhost:7088/api/orders" \
  -H "Authorization: Bearer <your-jwt-token>" \
  -H "Content-Type: application/json" \
  -d '{
    "shippingAddress": "123 Main St, City, State 12345",
    "billingAddress": "123 Main St, City, State 12345",
    "notes": "Please deliver during business hours",
    "couponId": 1,
    "orderItems": [
      {
        "productId": 1,
        "quantity": 2
      }
    ]
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
- Product names: 1-100 characters
- Product descriptions: 1-500 characters
- SKU: 1-20 characters, alphanumeric with hyphens and underscores
- Order addresses: 1-200 characters

## 🛍️ E-commerce Features

### Product Management
- **Product Catalog**: Complete product information with images, descriptions, and pricing
- **Category System**: Hierarchical categorization for easy product organization
- **Inventory Tracking**: Real-time stock quantity management
- **SKU Management**: Unique product identifiers for inventory control
- **Brand Management**: Organize products by manufacturer/brand

### Order Management
- **Order Processing**: Complete order lifecycle from creation to delivery
- **Status Tracking**: Track orders through Pending → Processing → Shipped → Delivered
- **Coupon Integration**: Apply discounts to orders using existing coupon system
- **User Orders**: Users can view their order history and current orders
- **Order Items**: Detailed breakdown of products in each order

### Business Logic
- **Stock Validation**: Ensure sufficient inventory before order processing
- **Price Calculation**: Automatic calculation of totals with coupon discounts
- **Order Number Generation**: Unique order identifiers for tracking
- **Status Management**: Admin controls for order status updates

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

### E-commerce Module Addition (Latest)
- **Products Module**: Complete product catalog with categories, brands, and inventory management
- **Orders Module**: Full order processing system with status tracking and coupon integration
- **Enhanced Models**: New entities for Product, Category, Order, and OrderItem
- **Business Logic**: Stock validation, price calculation, and order lifecycle management
- **Seeded Data**: Sample products and categories for immediate testing

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
- **Business Growth**: Project now supports full e-commerce operations
