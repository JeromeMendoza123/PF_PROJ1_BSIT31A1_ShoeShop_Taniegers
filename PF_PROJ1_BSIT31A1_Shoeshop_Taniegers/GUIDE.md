Repository Layer Developer Guide
Project Overview
This guide covers the setup and implementation of the Repository Layer for the Shoe Inventory Management System using ASP.NET Core, Entity Framework Core, and SQLite.
---

1. Project Structure
•	ShoeShop.Repository: Class library containing entity models, DbContext, and data access logic.
•	ShoeShop.Repository.Console: Console app for testing CRUD operations and database setup.

-----------------------------------------------------------------------------------------------------------------
2. TABLES


1.Shoe 


Properties:


Id (int, PK)

Name (string)

Brand (string)

Cost (decimal)

Price (decimal)

Description (string?)

ImageUrl (string?)

IsActive (bool)

CreatedDate (DateTime)


CONSTRAINTS/NOTES for Shoe Table:


Name and Brand are required

IsActive used for soft delete


---------------------

2. ShoeColorVariation

Properties:

Id (int, PK)

ShoeId (int, FK)

ColorName (string)

HexCode (string)

StockQuantity (int)

ReorderLevel (int, default 5)

IsActive (bool)


CONSTRAINTS/NOTES For ShoeColorVariation


Links to Shoe

Tracks stock

---------------------


3.Supplier

Properties:

Id (int, PK)

Name (string)

ContactEmail (string)

ContactPhone (string)

Address (string)

IsActive (bool)


CONSTRAINTS/NOTES for Supplier Table:


Supplier info


---------------------


4.PurchaseOrder

Properties:

Id (int, PK)

OrderNumber (string)

SupplierId (int, FK)

OrderDate (DateTime)

ExpectedDate (DateTime)

Status (enum)

TotalAmount (decimal)


CONSTRAINTS/NOTES for PurchaseOrder Table:


Status values: Pending, Confirmed, Shipped, Received, Cancelled


---------------------

5. PurchaseOrderItem


Properties:

Id (int, PK)

PurchaseOrderId (int, FK)

ShoeColorVariationId (int, FK)

QuantityOrdered (int)

QuantityReceived (int)

UnitCost (decimal)


CONSTRAINTS/NOTES for PurchaseOrderItem Table:


Links to PurchaseOrder and ShoeColorVariation


---------------------

6. StockPullOut


Properties:

Id (int, PK)

ShoeColorVariationId (int, FK)

Quantity (int)

Reason (string)

ReasonDetails (string?)

RequestedBy (string)

ApprovedBy (string?)

PullOutDate (DateTime)

Status (enum)


CONSTRAINTS/NOTES for StockPullOut Table:


Status values: Pending, Approved, Completed, Rejected

Navigation properties are set up for all relationships.

Enums are used for order and pull-out status.


-----------------------------------------------------------------------------------------------------------------







3. Packages Installed
For both projects, install via NuGet:
•	Microsoft.EntityFrameworkCore
•	Microsoft.EntityFrameworkCore.Sqlite
•	Microsoft.EntityFrameworkCore.Tools
•	Microsoft.Extensions.DependencyInjection
•	Microsoft.Extensions.Configuration
•	Microsoft.Extensions.Configuration.Json


1. 
4. DbContext Configuration
•	All entities are registered as DbSet in ShoeShopDbContext.
•	Relationships are configured using Fluent API in OnModelCreating.
•	SQLite is used for development:
Connection string: "Data Source=shoeshop.db"
•	Seed data is provided for 15 shoes, 3 suppliers, color variations, purchase orders, and pull-outs.



5. appsettings.json added code:

{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=shoeshop.db"
  }
}




6. Console Commands Used:

Add-Migration InitialCreate
Update-Database




7. Testing CRUD Operations
Sample code is in ShoeShop.Repository.Console\Program.cs:
•	List all shoes and suppliers
•	Add, update, and delete shoes
•	Query low stock color variations



8. .gitignore
Make sure .vs/, /bin, /obj, and other temp/build folders are ignored.
.vs/ should be included in the .gitignore file.



9. Commit & Pull Request Workflow
•	Make at least 10 meaningful commits on your feature branch (e.g., dev-yourname).
•	Push your branch to GitHub.
•	Create a pull request to main in your group’s repository.
•	Wait for a teammate to review and approve before merging.
•	Repeat for additional changes (e.g., documentation).



10. Documentation
•	Main documentation is in docs/repository-layer-docs.md.
•	This guide (repository-layer-guide.md) provides setup and process details for new team members.



11. Additional Notes
•	Do not commit .vs/ or other temp files.
•	All code changes must go through pull requests.
•	Use clear, descriptive commit messages.
•	Test CRUD operations in the console app before integration.