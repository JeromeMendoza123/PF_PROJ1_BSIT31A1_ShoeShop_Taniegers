using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services.DTOs;
using ShoeShop.Services.Interfaces;

namespace ShoeShop.Web.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly IPurchaseOrderService _service;

        public PurchaseOrdersController(IPurchaseOrderService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var orders = _service.GetAllOrders();
            return View(orders);
        }

        [HttpGet]
        public IActionResult GetOrder(int id)
        {
            var order = _service.GetOrderById(id);
            if (order == null) return Json(new { success = false, message = "Order not found" });
            return Json(order);
        }

        [HttpPost]
        public IActionResult Create(CreatePurchaseOrderDto dto)
        {
            if (!ModelState.IsValid) return Json(new { success = false, message = "Please fill all required fields." });

            _service.AddOrder(dto);
            return Json(new { success = true, message = "Order added successfully!" });
        }

        [HttpPost]
        public IActionResult Edit(int id, CreatePurchaseOrderDto dto)
        {
            _service.UpdateOrder(id, dto);
            return Json(new { success = true, message = "Order updated successfully!" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _service.DeleteOrder(id);
            return Json(new { success = true, message = "Order deleted successfully!" });
        }
    }
}
