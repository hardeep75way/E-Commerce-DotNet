# E-Commerce Application

A robust E-Commerce web application built with ASP.NET Core MVC, Entity Framework Core, and MySQL. It features a complete shopping experience for customers and a comprehensive admin panel for product management.

## 🚀 Features

### Customer Features
- **Product Catalog**: Browse and search products with pagination and category filtering.
- **Shopping Cart**: Add, remove, and update product quantities.
- **Order Management**: Review cart and place orders securely.
- **Responsive UI**: Clean, modern interface built with Bootstrap 5.

### Admin Panel
- **Product Management**: CRUD operations for products.
- **Excel Upload**: Bulk upload products using Excel files.
- **Order Overview**: (Future implementation) View and manage customer orders.
- **Secure Access**: Role-Based Access Control (RBAC) protecting admin routes.

### Authentication & Security
- **Cookie-Based Authentication**: Secure login and session management.
- **Role-Based Access Control (RBAC)**: Distinct roles for **Admin** and **Customer**.
- **Data Protection**: Password hashing and secure data storage.

## 🛠 Tech Stack

- **Framework**: ASP.NET Core 8 MVC
- **Database**: MySQL / MariaDB
- **ORM**: Entity Framework Core
- **Frontend**: Razor Views, Bootstrap 5, jQuery
- **Tools**: ExcelDataReader (for bulk uploads)

## ⚙️ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) 

### 1. Database Configuration
Ensure your connection string in `appsettings.json` points to your MySQL instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=ecommerce_db;user=root;password=your_password"
}
```

### 2. Apply Migrations
Run the following commands in the project root to set up the database and seed initial data:

```bash
# Navigate to the project folder
cd E-Commerce

# Add the Auth migration (if not already done)
dotnet ef migrations add AddUsersTable

# Update database
dotnet ef database update
```

### 3. Run the Application
```bash
dotnet run
```
Access the application at `https://localhost:7196` (or the port shown in your terminal).

## 📂 Project Structure

- **Domain**: Entities and Enums (`User`, `Product`, `Order`)
- **Infrastructure**: generic Repositories, `AppDbContext`, Database logic.
- **Application**: Business logic, Services (`AuthService`, `ProductService`), DTOs.
- **Web (Root)**: Controllers, Views, ViewModels.
  - `Areas/Admin`: Admin-specific controllers and views.
  - `Areas/Customer`: Customer-facing controllers and views.

## 📝 License
This project is for educational purposes.
