using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaGestionDeTaller.Areas.Main.Models;
using SistemaGestionDeTaller.Models;
using System.Linq;

namespace SistemaGestionDeTaller.Areas.Main.Controllers
{
    [Area("Main")]
    public class UtilitiesController : Controller
    {
        private readonly DbstContext _context;

        public UtilitiesController(DbstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dayNow = DateTime.Now.Date;

            var dayNowMin = new DateTime(dayNow.Year, dayNow.Month, dayNow.Day, 0, 0, 0);
            var dayNowMax = new DateTime(dayNow.Year, dayNow.Month, dayNow.Day, 23, 59, 59);

            var saleNow = await _context.Sales.Where(s => s.Date >= dayNowMin && s.Date <= dayNowMax).ToListAsync();
            var productsStock = await _context.Products.Where(p => p.Stock == 0).ToListAsync();
            var servicesNow = await _context.Services.Where(s => s.Date >= dayNowMin && s.Date <= dayNowMax).ToListAsync();

            var list = new Utilities
            {
                SaleList = saleNow,
                ProductsList = productsStock,
                ServicesList = servicesNow
            };

            return View(list);
        }
    }
}