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
    public class ShoeService : IShoeService
    {
        private readonly ShoeShopDbContext _context;

        public ShoeService(ShoeShopDbContext context)
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
                    Name = s.Name ?? string.Empty,
                    Brand = s.Brand ?? string.Empty,
                    Price = s.Price,
                    Cost = s.Cost,
                    Description = s.Description ?? string.Empty,
                    ImageUrl = s.ImageUrl
                }).ToListAsync();
        }

        public async Task<ShoeDto?> GetShoeByIdAsync(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null || !shoe.IsActive) return null;

            return new ShoeDto
            {
                Id = shoe.Id,
                Name = shoe.Name ?? string.Empty,
                Brand = shoe.Brand ?? string.Empty,
                Price = shoe.Price,
                Cost = shoe.Cost,
                Description = shoe.Description ?? string.Empty,
                ImageUrl = shoe.ImageUrl
            };
        }

        public async Task AddShoeAsync(CreateShoeDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var shoe = new Shoe
            {
                Name = dto.Name ?? string.Empty,
                Brand = dto.Brand ?? string.Empty,
                Price = dto.Price,
                Cost = dto.Cost,
                Description = dto.Description ?? string.Empty,
                ImageUrl = dto.ImageUrl,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _context.Shoes.Add(shoe);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateShoeAsync(int id, CreateShoeDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null || !shoe.IsActive) return;

            shoe.Name = dto.Name ?? string.Empty;
            shoe.Brand = dto.Brand ?? string.Empty;
            shoe.Price = dto.Price;
            shoe.Cost = dto.Cost;
            shoe.Description = dto.Description ?? string.Empty;
            shoe.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteShoeAsync(int id)
        {
            var shoe = await _context.Shoes.FindAsync(id);
            if (shoe == null || !shoe.IsActive) return;

            shoe.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}
