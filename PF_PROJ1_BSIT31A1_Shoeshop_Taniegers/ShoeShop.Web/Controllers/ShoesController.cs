using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Web.Controllers
{
    public class ShoesController : Controller
    {
        private static readonly List<ShoeDto> _shoes = new()
        {
            new ShoeDto { Id = 1, Name = "Runner", Brand = "Nike", Price = 2999m, Cost = 2000m, ImageUrl="/images/shoe1.jpg", Description="Comfortable running shoes" },
            new ShoeDto { Id = 2, Name = "Classic", Brand = "Adidas", Price = 2499m, Cost = 1800m, ImageUrl="/images/shoe2.jpg", Description="Timeless style" },
            new ShoeDto { Id = 3, Name = "Sneaker", Brand = "Puma", Price = 1999m, Cost = 1500m, ImageUrl="/images/shoe3.jpg", Description="Casual everyday wear" }
        };

        public IActionResult Index()
        {
            return View(_shoes);
        }

        [HttpPost]
        public IActionResult Create(CreateShoeDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Brand))
            {
                return Json(new { success = false, message = "Please fill all required fields." });
            }

            var newShoe = new ShoeDto
            {
                Id = _shoes.Any() ? _shoes.Max(s => s.Id) + 1 : 1,
                Name = dto.Name!,   // safe because we validated
                Brand = dto.Brand!,
                Cost = dto.Cost,
                Price = dto.Price,
                Description = dto.Description ?? string.Empty,
                ImageUrl = dto.ImageUrl ?? string.Empty
            };

            _shoes.Add(newShoe);

            return Json(new { success = true, message = $"{newShoe.Name} added successfully!" });
        }

        [HttpPost]
        public IActionResult Edit(CreateShoeDto dto, int id)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Brand))
            {
                return Json(new { success = false, message = "Please fill all required fields." });
            }

            var shoe = _shoes.FirstOrDefault(s => s.Id == id);
            if (shoe == null)
                return Json(new { success = false, message = "Shoe not found!" });

            shoe.Name = dto.Name!;
            shoe.Brand = dto.Brand!;
            shoe.Cost = dto.Cost;
            shoe.Price = dto.Price;
            shoe.Description = dto.Description ?? string.Empty;
            shoe.ImageUrl = dto.ImageUrl ?? string.Empty;

            return Json(new { success = true, message = $"{shoe.Name} updated successfully!" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var shoe = _shoes.FirstOrDefault(s => s.Id == id);
            if (shoe != null)
                _shoes.Remove(shoe);

            return Json(new { success = true, message = "Shoe deleted successfully!" });
        }

        [HttpGet]
        public IActionResult GetShoe(int id)
        {
            var shoe = _shoes.FirstOrDefault(s => s.Id == id);
            if (shoe == null)
                return Json(new { success = false, message = "Shoe not found" });

            return Json(shoe);
        }
    }
}
