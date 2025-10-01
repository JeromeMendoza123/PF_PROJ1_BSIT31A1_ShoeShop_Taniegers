using Microsoft.EntityFrameworkCore;
using ShoeShop.Repository.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShoeShop.Repository.Entities;

namespace ShoeShop.Repository
{

    public class ShoeShopDbContext : IdentityDbContext<ApplicationUser>
    {
  
        public ShoeShopDbContext(DbContextOptions<ShoeShopDbContext> options) : base(options) { }
        public ShoeShopDbContext() { }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<ShoeColorVariation> ShoeColorVariations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<StockPullOut> StockPullOuts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=shoeshop.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Shoe - ShoeColorVariation (1:M)
            modelBuilder.Entity<Shoe>()
                .HasMany(s => s.ColorVariations)
                .WithOne(cv => cv.Shoe)
                .HasForeignKey(cv => cv.ShoeId)
                .OnDelete(DeleteBehavior.Cascade);

            // ShoeColorVariation - PurchaseOrderItem (1:M)
            modelBuilder.Entity<ShoeColorVariation>()
                .HasMany(cv => cv.PurchaseOrderItems)
                .WithOne(poi => poi.ShoeColorVariation)
                .HasForeignKey(poi => poi.ShoeColorVariationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier - PurchaseOrder (1:M)
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.PurchaseOrders)
                .WithOne(po => po.Supplier)
                .HasForeignKey(po => po.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseOrder - PurchaseOrderItem (1:M)
            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.Items)
                .WithOne(poi => poi.PurchaseOrder)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ShoeColorVariation - StockPullOut (1:M)
            modelBuilder.Entity<ShoeColorVariation>()
                .HasMany(cv => cv.StockPullOuts)
                .WithOne(spo => spo.ShoeColorVariation)
                .HasForeignKey(spo => spo.ShoeColorVariationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Suppliers
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "Nike Supplier", ContactEmail = "nike@supplier.com", ContactPhone = "1234567890", Address = "Nike HQ", IsActive = true },
                new Supplier { Id = 2, Name = "Adidas Supplier", ContactEmail = "adidas@supplier.com", ContactPhone = "2345678901", Address = "Adidas HQ", IsActive = true },
                new Supplier { Id = 3, Name = "Puma Supplier", ContactEmail = "puma@supplier.com", ContactPhone = "3456789012", Address = "Puma HQ", IsActive = true }
            );

            // Seed Shoes
            modelBuilder.Entity<Shoe>().HasData(
                new Shoe { Id = 1, Name = "Nike Air Max", Brand = "Nike", Cost = 100, Price = 150, Description = "Classic running shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 2, Name = "Adidas Ultra Boost", Brand = "Adidas", Cost = 110, Price = 160, Description = "Comfortable boost sole.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 3, Name = "Puma RS-X", Brand = "Puma", Cost = 90, Price = 140, Description = "Retro style sneaker.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 4, Name = "Nike Revolution", Brand = "Nike", Cost = 80, Price = 120, Description = "Affordable running shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 5, Name = "Adidas Stan Smith", Brand = "Adidas", Cost = 85, Price = 130, Description = "Classic tennis shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 6, Name = "Puma Suede Classic", Brand = "Puma", Cost = 70, Price = 110, Description = "Iconic suede sneaker.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 7, Name = "Nike Air Force 1", Brand = "Nike", Cost = 95, Price = 145, Description = "Legendary basketball shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 8, Name = "Adidas Gazelle", Brand = "Adidas", Cost = 75, Price = 115, Description = "Timeless style.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 9, Name = "Puma Future Rider", Brand = "Puma", Cost = 88, Price = 135, Description = "Modern runner.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 10, Name = "Nike Pegasus", Brand = "Nike", Cost = 105, Price = 155, Description = "Versatile running shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 11, Name = "Adidas NMD", Brand = "Adidas", Cost = 120, Price = 170, Description = "Street style sneaker.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 12, Name = "Puma Cali", Brand = "Puma", Cost = 95, Price = 145, Description = "Casual everyday shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 13, Name = "Nike Zoom Fly", Brand = "Nike", Cost = 115, Price = 165, Description = "Fast racing shoe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 14, Name = "Adidas Superstar", Brand = "Adidas", Cost = 90, Price = 140, Description = "Classic shell toe.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now },
                new Shoe { Id = 15, Name = "Puma Cell Endura", Brand = "Puma", Cost = 100, Price = 150, Description = "Chunky retro runner.", ImageUrl = null, IsActive = true, CreatedDate = DateTime.Now }
            );

            // Seed ShoeColorVariations
            modelBuilder.Entity<ShoeColorVariation>().HasData(
                new ShoeColorVariation { Id = 1, ShoeId = 1, ColorName = "Red", HexCode = "#FF0000", StockQuantity = 10, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 2, ShoeId = 1, ColorName = "Blue", HexCode = "#0000FF", StockQuantity = 8, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 3, ShoeId = 2, ColorName = "White", HexCode = "#FFFFFF", StockQuantity = 12, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 4, ShoeId = 2, ColorName = "Black", HexCode = "#000000", StockQuantity = 7, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 5, ShoeId = 3, ColorName = "Green", HexCode = "#00FF00", StockQuantity = 9, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 6, ShoeId = 4, ColorName = "Grey", HexCode = "#808080", StockQuantity = 6, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 7, ShoeId = 5, ColorName = "Yellow", HexCode = "#FFFF00", StockQuantity = 11, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 8, ShoeId = 6, ColorName = "Pink", HexCode = "#FFC0CB", StockQuantity = 5, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 9, ShoeId = 7, ColorName = "Purple", HexCode = "#800080", StockQuantity = 4, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 10, ShoeId = 8, ColorName = "Orange", HexCode = "#FFA500", StockQuantity = 3, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 11, ShoeId = 9, ColorName = "Brown", HexCode = "#A52A2A", StockQuantity = 2, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 12, ShoeId = 10, ColorName = "Cyan", HexCode = "#00FFFF", StockQuantity = 1, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 13, ShoeId = 11, ColorName = "Magenta", HexCode = "#FF00FF", StockQuantity = 15, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 14, ShoeId = 12, ColorName = "Lime", HexCode = "#BFFF00", StockQuantity = 14, ReorderLevel = 5, IsActive = true },
                new ShoeColorVariation { Id = 15, ShoeId = 13, ColorName = "Teal", HexCode = "#008080", StockQuantity = 13, ReorderLevel = 5, IsActive = true }
            );

            // Seed PurchaseOrders
            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder { Id = 1, OrderNumber = "PO-1001", SupplierId = 1, OrderDate = DateTime.Now.AddDays(-10), ExpectedDate = DateTime.Now.AddDays(-5), Status = PurchaseOrderStatus.Received, TotalAmount = 1500 },
                new PurchaseOrder { Id = 2, OrderNumber = "PO-1002", SupplierId = 2, OrderDate = DateTime.Now.AddDays(-8), ExpectedDate = DateTime.Now.AddDays(-3), Status = PurchaseOrderStatus.Received, TotalAmount = 1600 },
                new PurchaseOrder { Id = 3, OrderNumber = "PO-1003", SupplierId = 3, OrderDate = DateTime.Now.AddDays(-6), ExpectedDate = DateTime.Now.AddDays(-1), Status = PurchaseOrderStatus.Received, TotalAmount = 1400 }
            );

            // Seed PurchaseOrderItems
            modelBuilder.Entity<PurchaseOrderItem>().HasData(
                new PurchaseOrderItem { Id = 1, PurchaseOrderId = 1, ShoeColorVariationId = 1, QuantityOrdered = 10, QuantityReceived = 10, UnitCost = 100 },
                new PurchaseOrderItem { Id = 2, PurchaseOrderId = 1, ShoeColorVariationId = 2, QuantityOrdered = 8, QuantityReceived = 8, UnitCost = 100 },
                new PurchaseOrderItem { Id = 3, PurchaseOrderId = 2, ShoeColorVariationId = 3, QuantityOrdered = 12, QuantityReceived = 12, UnitCost = 110 },
                new PurchaseOrderItem { Id = 4, PurchaseOrderId = 2, ShoeColorVariationId = 4, QuantityOrdered = 7, QuantityReceived = 7, UnitCost = 110 },
                new PurchaseOrderItem { Id = 5, PurchaseOrderId = 3, ShoeColorVariationId = 5, QuantityOrdered = 9, QuantityReceived = 9, UnitCost = 90 }
            );

            // Seed StockPullOuts
            modelBuilder.Entity<StockPullOut>().HasData(
                new StockPullOut { Id = 1, ShoeColorVariationId = 1, Quantity = 2, Reason = "Damaged", ReasonDetails = "Box damaged in transit", RequestedBy = "Staff1", ApprovedBy = "Manager1", PullOutDate = DateTime.Now.AddDays(-2), Status = StockPullOutStatus.Completed },
                new StockPullOut { Id = 2, ShoeColorVariationId = 3, Quantity = 1, Reason = "Promotional", ReasonDetails = "Display pair", RequestedBy = "Staff2", ApprovedBy = "Manager2", PullOutDate = DateTime.Now.AddDays(-1), Status = StockPullOutStatus.Completed }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

//seed data code added