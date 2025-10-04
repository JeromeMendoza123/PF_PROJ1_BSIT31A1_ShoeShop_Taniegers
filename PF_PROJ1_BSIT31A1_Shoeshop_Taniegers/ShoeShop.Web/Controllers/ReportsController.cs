using Microsoft.AspNetCore.Mvc;
using ShoeShop.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Web.Controllers
{
    public class ReportsController : Controller
    {
        private static List<InventoryReportDto> reports = new()
        {
            new InventoryReportDto { Id = 1, Name="Air Max", Brand="Nike", Cost=50, Price=100, Description="Running shoe", ImageUrl="" },
            new InventoryReportDto { Id = 2, Name="UltraBoost", Brand="Adidas", Cost=60, Price=120, Description="Comfortable shoe", ImageUrl="" }
        };
        private static int nextId = 3;

        public IActionResult Index()
        {
            return View(reports);
        }

        [HttpGet]
        public IActionResult GetReport(int id)
        {
            var report = reports.FirstOrDefault(r => r.Id == id);
            if (report == null) return Json(null);

            return Json(report);
        }

        [HttpPost]
        public IActionResult Create(InventoryReportDto report)
        {
            report.Id = nextId++;
            reports.Add(report);
            return Json(new { success = true, message = "Report added successfully!" });
        }

        [HttpPost]
        public IActionResult Edit(InventoryReportDto report)
        {
            var existing = reports.FirstOrDefault(r => r.Id == report.Id);
            if (existing != null)
            {
                existing.Name = report.Name;
                existing.Brand = report.Brand;
                existing.Cost = report.Cost;
                existing.Price = report.Price;
                existing.Description = report.Description;
                existing.ImageUrl = report.ImageUrl;
            }
            return Json(new { success = true, message = "Report updated successfully!" });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var report = reports.FirstOrDefault(r => r.Id == id);
            if (report != null) reports.Remove(report);

            return Json(new { success = true, message = "Report deleted successfully!" });
        }
    }
}
