using Microsoft.Extensions.DependencyInjection;
using ShoeShop.Repository;
using ShoeShop.Repository.Entities;
using ShoeShop.Services;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces; 
using ShoeShop.Services.Services;  
using System;
using System.Linq;
using System.Threading.Tasks;


class Program
{
    static async Task Main()
    {
        var services = new ServiceCollection();
        services.AddDbContext<ShoeShopDbContext>();
        services.AddScoped<IInventoryService, InventoryService>();
        // Add other services as needed

        var provider = services.BuildServiceProvider();
        var inventoryService = provider.GetRequiredService<IInventoryService>();
        var context = provider.GetRequiredService<ShoeShopDbContext>();

        // 1. List all shoes (via service)
        Console.WriteLine("All Shoes:");
        var shoes = await inventoryService.GetAllShoesAsync();
        foreach (var shoe in shoes)
        {
            Console.WriteLine($"{shoe.Id}: {shoe.Name} ({shoe.Brand}) - Price: {shoe.Price}");
        }

        // 2. List all suppliers (direct from context)
        Console.WriteLine("\nAll Suppliers:");
        foreach (var supplier in context.Suppliers)
        {
            Console.WriteLine($"{supplier.Id}: {supplier.Name} - Email: {supplier.ContactEmail}");
        }

        // 3. Add a new shoe (via service)
        var newShoeDto = new ShoeShop.Services.DTOs.CreateShoeDto
        {
            Name = "Test Shoe",
            Brand = "Test Brand",
            Cost = 50,
            Price = 80,
            Description = "Sample shoe for testing.",
            ImageUrl = null
        };
        await inventoryService.AddShoeAsync(newShoeDto);
        Console.WriteLine($"\nAdded new shoe: {newShoeDto.Name}");

        // 4. Update a shoe's price (direct from context for demo)
        var shoeToUpdate = context.Shoes.FirstOrDefault(s => s.Name == "Nike Air Max");
        if (shoeToUpdate != null)
        {
            shoeToUpdate.Price = 175;
            context.SaveChanges();
            Console.WriteLine($"\nUpdated price for {shoeToUpdate.Name} to {shoeToUpdate.Price}");
        }

        // 5. Delete a shoe (via service)
        var shoeToDelete = context.Shoes.FirstOrDefault(s => s.Name == "Test Shoe");
        if (shoeToDelete != null)
        {
            await inventoryService.DeleteShoeAsync(shoeToDelete.Id);
            Console.WriteLine($"\nDeleted shoe: {shoeToDelete.Name}");
        }

        // 6. Query low stock color variations (direct from context)
        Console.WriteLine("\nLow Stock Color Variations:");
        var lowStock = context.ShoeColorVariations
            .Where(cv => cv.StockQuantity <= cv.ReorderLevel)
            .ToList();
        foreach (var cv in lowStock)
        {
            Console.WriteLine($"{cv.ColorName} of ShoeId {cv.ShoeId} - Stock: {cv.StockQuantity}");
        }
    }
}

// test succeeded// test succeeded