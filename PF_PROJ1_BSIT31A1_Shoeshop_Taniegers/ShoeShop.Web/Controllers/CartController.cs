using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services;

namespace ShoeShop.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int shoeId)
        {
            _cartService.RemoveFromCart(shoeId);
            TempData["SuccessMessage"] = "Item removed from cart!";
            return RedirectToAction("Index");
        }
    }
}
