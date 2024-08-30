using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SistemaGestionDeTaller.Areas.Main.Models;
using SistemaGestionDeTaller.Areas.Main.Models.ViewModel;
using SistemaGestionDeTaller.Models;
using System.Diagnostics;

namespace SistemaGestionDeTaller.Areas.Main.Controllers
{
    [Area("Main")]
    public class ReportController : Controller
    {
        private readonly DbstContext _context;

        public ReportController(DbstContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ReportSale(int id)
        {
            var dataSelect = await _context.Data.FirstOrDefaultAsync();

            var saleSelect = await _context.Sales.Where(s => s.Id == id).FirstOrDefaultAsync();

            List<Concept> conceptList = await _context.Concepts.Where(c => c.SaleNumber == id).ToListAsync();

            if(dataSelect != null && saleSelect != null)
            {
                ReportSaleModel model = new()
                {
                    DataConfig = dataSelect,
                    SaleObjetct = saleSelect,
                    ConceptsList = conceptList
                };
                return new ViewAsPdf("ReportSale", model)
                {
                    FileName = $"Venta--{model.SaleObjetct.Id}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4
                };
            }
            else
            {
                return RedirectToAction("Index", "Sales");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReportService(int id)
        {
            var dataSelect = await _context.Data.FirstOrDefaultAsync();
            var service = await _context.Services.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (dataSelect != null && service != null)
            {
                ReportServiceModel model = new ReportServiceModel
                {
                    DataConfig = dataSelect,
                    ServiceObjetct = service
                };
                return new ViewAsPdf("ReportService", model)
                {
                    FileName = $"Service--{model.ServiceObjetct.Id}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4
                };
            }
            else { return RedirectToAction("Index", "Services"); }
        }
    }
}