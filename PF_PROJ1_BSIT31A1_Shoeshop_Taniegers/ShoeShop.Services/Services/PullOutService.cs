using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoeShop.Repository;
using ShoeShop.Repository.Entities;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;

namespace ShoeShop.Services.Services
{
    public class PullOutService : IPullOutService
    {
        private readonly ShoeShopDbContext _context;

        public PullOutService(ShoeShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<ShoeDto>> GetAllShoesAsync()
        {
            return await _context.Shoes
                .Where(s => s.IsActive)
                .Select(s => new ShoeDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Brand = s.Brand,
                    Price = s.Price,
                    Cost = s.Cost,
                    Description = s.Description ?? "",
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<ShoeDto?> GetShoeByIdAsync(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe is not { IsActive: true }) return null;

            return new ShoeDto
            {
                Id = shoe.Id,
                Name = shoe.Name,
                Brand = shoe.Brand,
                Price = shoe.Price,
                Cost = shoe.Cost,
                Description = shoe.Description ?? "",
                ImageUrl = shoe.ImageUrl
            };
        }

        public async Task AddShoeAsync(CreateShoeDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var shoe = new Shoe
            {
                Name = dto.Name ?? "",
                Brand = dto.Brand ?? "",
                Cost = dto.Cost,
                Price = dto.Price,
                Description = dto.Description ?? "",
                ImageUrl = dto.ImageUrl,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShoeAsync(int id, CreateShoeDto dto)
        {
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe is not { IsActive: true }) return;

            shoe.Name = dto.Name ?? "";
            shoe.Brand = dto.Brand ?? "";
            shoe.Cost = dto.Cost;
            shoe.Price = dto.Price;
            shoe.Description = dto.Description ?? "";
            shoe.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteShoeAsync(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe is not { IsActive: true }) return;

            shoe.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}
