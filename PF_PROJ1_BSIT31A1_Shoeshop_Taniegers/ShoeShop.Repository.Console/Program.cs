using Microsoft.Extensions.DependencyInjection;
using ShoeShop.Repository;
using ShoeShop.Repository.Entities;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddDbContext<ShoeShopDbContext>();
        var provider = services.BuildServiceProvider();

        using var context = provider.GetRequiredService<ShoeShopDbContext>();

        // 1. List all shoes
        Console.WriteLine("All Shoes:");
        foreach (var shoe in context.Shoes)
        {
            Console.WriteLine($"{shoe.Id}: {shoe.Name} ({shoe.Brand}) - Price: {shoe.Price}");
        }

        // 2. List all suppliers
        Console.WriteLine("\nAll Suppliers:");
        foreach (var supplier in context.Suppliers)
        {
            Console.WriteLine($"{supplier.Id}: {supplier.Name} - Email: {supplier.ContactEmail}");
        }

        // 3. Add a new shoe
        var newShoe = new Shoe
        {
            Name = "Test Shoe",
            Brand = "Test Brand",
            Cost = 50,
            Price = 80,
            Description = "Sample shoe for testing.",
            IsActive = true,
            CreatedDate = DateTime.Now
        };
        context.Shoes.Add(newShoe);
        context.SaveChanges();
        Console.WriteLine($"\nAdded new shoe: {newShoe.Name}");

        // 4. Update a shoe's price
        var shoeToUpdate = context.Shoes.FirstOrDefault(s => s.Name == "Nike Air Max");
        if (shoeToUpdate != null)
        {
            shoeToUpdate.Price = 175;
            context.SaveChanges();
            Console.WriteLine($"\nUpdated price for {shoeToUpdate.Name} to {shoeToUpdate.Price}");
        }

        // 5. Delete a shoe
        var shoeToDelete = context.Shoes.FirstOrDefault(s => s.Name == "Test Shoe");
        if (shoeToDelete != null)
        {
            context.Shoes.Remove(shoeToDelete);
            context.SaveChanges();
            Console.WriteLine($"\nDeleted shoe: {shoeToDelete.Name}");
        }

        // 6. Query low stock color variations
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

// test succeeded