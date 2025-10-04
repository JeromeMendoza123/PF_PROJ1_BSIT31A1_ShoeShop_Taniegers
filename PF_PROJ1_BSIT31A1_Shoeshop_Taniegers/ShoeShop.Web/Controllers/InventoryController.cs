using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;
using System.Threading.Tasks;

namespace ShoeShop.Web.Controllers
{
    // InventoryController: Handles all shoe and stock management operations
    // Student D: Added TempData error/success message handling for smoother user feedback during runtime.
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Displays inventory list and shows any TempData messages
        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            var shoes = await _inventoryService.GetAllShoesAsync();
            return View(shoes);
        }

        // Shows details for a specific shoe
        public async Task<IActionResult> Details(int id)
        {
            var shoe = await _inventoryService.GetShoeByIdAsync(id);
            if (shoe == null)
            {
                TempData["ErrorMessage"] = "Shoe not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // Returns create shoe form
        public IActionResult Create()
        {
            return View();
        }

        // Handles shoe creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShoeDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                return View(dto);
            }

            await _inventoryService.AddShoeAsync(dto);
            TempData["SuccessMessage"] = "Shoe created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // Returns edit shoe form
        public async Task<IActionResult> Edit(int id)
        {
            var shoe = await _inventoryService.GetShoeByIdAsync(id);
            if (shoe == null)
            {
                TempData["ErrorMessage"] = "Shoe not found.";
                return RedirectToAction(nameof(Index));
            }

            var dto = new CreateShoeDto
            {
                Name = shoe.Name,
                Brand = shoe.Brand,
                Price = shoe.Price,
                Cost = shoe.Cost,
                Description = shoe.Description,
                ImageUrl = shoe.ImageUrl
            };
            return View(dto);
        }

        // Handles shoe editing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateShoeDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors in the form.";
                return View(dto);
            }

            await _inventoryService.UpdateShoeAsync(id, dto);
            TempData["SuccessMessage"] = "Shoe updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // Handles shoe deletion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _inventoryService.DeleteShoeAsync(id);
            TempData["SuccessMessage"] = "Shoe deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
