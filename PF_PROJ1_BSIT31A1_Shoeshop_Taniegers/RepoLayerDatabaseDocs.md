Shoe Inventory Management System – Repository Layer Documentation

Overview

The Repository Layer is responsible for all data access and database operations in the Shoe Inventory Management System. It uses Entity Framework Core with a SQLite database for development and follows best practices for a clean, maintainable architecture.

Entity Models

Shoe
•	Id (int): Primary key
•	Name (string): Shoe name
•	Brand (string): Shoe brand
•	Cost (decimal): Purchase cost
•	Price (decimal): Selling price
•	Description (string, optional)
•	ImageUrl (string, optional)
•	IsActive (bool)
•	CreatedDate (DateTime)
•	Navigation: ColorVariations (collection of ShoeColorVariation)



ShoeColorVariation
•	Id (int): Primary key
•	ShoeId (int): Foreign key to Shoe
•	ColorName (string)
•	HexCode (string)
•	StockQuantity (int)
•	ReorderLevel (int, default 5)
•	IsActive (bool)
•	Navigation: Shoe, PurchaseOrderItems, StockPullOuts



Supplier
•	Id (int): Primary key
•	Name (string)
•	ContactEmail (string)
•	ContactPhone (string)
•	Address (string)
•	IsActive (bool)
•	Navigation: PurchaseOrders



PurchaseOrder
•	Id (int): Primary key
•	OrderNumber (string)
•	SupplierId (int): Foreign key to Supplier
•	OrderDate (DateTime)
•	ExpectedDate (DateTime)
•	Status (enum: Pending, Confirmed, Shipped, Received, Cancelled)
•	TotalAmount (decimal)
•	Navigation: Supplier, Items



PurchaseOrderItem
•	Id (int): Primary key
•	PurchaseOrderId (int): Foreign key to PurchaseOrder
•	ShoeColorVariationId (int): Foreign key to ShoeColorVariation
•	QuantityOrdered (int)
•	QuantityReceived (int)
•	UnitCost (decimal)
•	Navigation: PurchaseOrder, ShoeColorVariation



StockPullOut
•	Id (int): Primary key
•	ShoeColorVariationId (int): Foreign key to ShoeColorVariation
•	Quantity (int)
•	Reason (string)
•	ReasonDetails (string, optional)
•	RequestedBy (string)
•	ApprovedBy (string, optional)
•	PullOutDate (DateTime)
•	Status (enum: Pending, Approved, Completed, Rejected)
•	Navigation: ShoeColorVariation




DbContext Configuration
•	All entities are registered as DbSet properties in ShoeShopDbContext.
•	Relationships are configured using Fluent API in OnModelCreating.
•	SQLite is used for development via the connection string: Data Source=shoeshop.db.
•	Seed data is provided for demonstration (15 shoes, 3 suppliers, color variations, sample orders, and pull-outs)


.
Database Migrations
•	Initial migration creates all tables and relationships.
•	Seed data is applied during migration.
•	Use Add-Migration InitialCreate and Update-Database in Package Manager Console.



Testing CRUD Operations
CRUD operations and queries can be tested in the ShoeShop.Repository.Console app. Example operations:
•	List all shoes and suppliers
•	Add, update, and delete shoes
•	Query low stock color variations



How to Use
1.	Build the solution.
2.	Run migrations to create and seed the database.
3.	Use the console app to test CRUD operations.
4.	All changes are tracked via Git commits and pull requests.
	
	
Notes
•	The .vs/ folder and other build artifacts are ignored via .gitignore.
•	All code changes should be committed with clear messages and merged via pull requests to the main branch.