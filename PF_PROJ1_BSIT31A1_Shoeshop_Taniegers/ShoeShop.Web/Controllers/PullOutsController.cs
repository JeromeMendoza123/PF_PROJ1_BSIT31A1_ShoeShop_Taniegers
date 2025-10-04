using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Web.Controllers
{
    // PullOutsController: Handles stock removal operations and pull-out approval workflow
    public class PullOutsController : Controller
    {
        private static List<PullOutRequestDto> pullOuts = new();
        private static int nextId = 1;

        // Displays all pull-out requests
        public IActionResult Index()
        {
            return View(pullOuts);
        }

        // Gets a specific pull-out request (AJAX)
        [HttpGet]
        public IActionResult GetPullOut(int id)
        {
            var item = pullOuts.FirstOrDefault(p => p.Id == id);
            if (item == null) return Json(new { success = false, message = "Item not found" });
            return Json(new
            {
                success = true,
                id = item.Id,
                name = item.Name,
                brand = item.Brand,
                cost = item.Cost,
                price = item.Price,
                description = item.Description,
                imageUrl = item.ImageUrl
            });
        }

        // Creates a new pull-out request
        [HttpPost]
        public IActionResult Create(PullOutRequestDto request)
        {
            request.Id = nextId++;
            pullOuts.Add(request);
            return Json(new { success = true, message = "Pull out request added successfully!" });
        }

        // Edits an existing pull-out request
        [HttpPost]
        public IActionResult Edit(PullOutRequestDto request)
        {
            var existing = pullOuts.FirstOrDefault(p => p.Id == request.Id);
            if (existing != null)
            {
                existing.Name = request.Name;
                existing.Brand = request.Brand;
                existing.Cost = request.Cost;
                existing.Price = request.Price;
                existing.Description = request.Description;
                existing.ImageUrl = request.ImageUrl;
            }
            return Json(new { success = true, message = "Pull out request updated successfully!" });
        }

        // Deletes a pull-out request
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = pullOuts.FirstOrDefault(p => p.Id == id);
            if (item != null) pullOuts.Remove(item);
            return Json(new { success = true, message = "Pull out request deleted successfully!" });
        }
    }
}
