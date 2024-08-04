create database IMS3

use IMS3




 CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    CategoryId INT NOT NULL,
    SupplierId INT NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (SupplierId) REFERENCES Suppliers(SupplierId)
);

-------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Suppliers (
    SupplierId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(100),
    ContactEmail NVARCHAR(100),
    ContactPhone NVARCHAR(20),
    Address NVARCHAR(255),
    City NVARCHAR(100),
    Country NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME()
);

-------------------------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------

CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    City NVARCHAR(100),
    Country NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME()
);
Go

----------------------------------------------------------------------------------------------

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NOT NULL,
    OrderDate DATETIME2 DEFAULT SYSDATETIME(),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    OrderStatus NVARCHAR(50),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
Go

------------------------------------------------------------------------------------------------


CREATE TABLE OrderDetails (
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    TotalPrice AS (Quantity * UnitPrice),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
Go


---------------------------------------------------------------------------------------------------
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME()
);
Go

--------------------------------------------------------------------------------------------------


CREATE TABLE InventoryTransactions (
    TransactionId INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    TransactionType NVARCHAR(50) NOT NULL, -- e.g., 'Purchase', 'Sale', 'Adjustment'
    TransactionDate DATETIME2 DEFAULT SYSDATETIME(),
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME(),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
Go

------------------------------------------------------------------------------------------------------------------------------


CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
    Role NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 DEFAULT SYSDATETIME()
);
Go

------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE UserRoles (
    UserRoleId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSDATETIME(),
	UpdatedAt DATETIME DEFAULT SYSDATETIME(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
Go