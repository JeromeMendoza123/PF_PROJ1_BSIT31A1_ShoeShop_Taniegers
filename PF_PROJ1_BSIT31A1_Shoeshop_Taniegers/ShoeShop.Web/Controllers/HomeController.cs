using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;

namespace ShoeShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShoeService _shoeService;
        private readonly CartService _cartService;

        public HomeController(IShoeService shoeService, CartService cartService)
        {
            _shoeService = shoeService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all active shoes
            var shoes = await _shoeService.GetAllShoesAsync();
            return View(shoes);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var shoe = _shoeService.GetShoeByIdAsync(id).Result; // get single shoe
            if (shoe == null)
                return Json(new { success = false, message = "Shoe not found." });

            _cartService.AddToCart(shoe, quantity);
            return Json(new { success = true, message = $"{shoe.Name} added to cart!" });
        }
    }
}
