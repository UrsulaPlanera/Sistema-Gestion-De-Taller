using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SistemaGestionDeTaller.Areas.Main.Models;
using SistemaGestionDeTaller.Models;

namespace SistemaGestionDeTaller.Areas.Main.Controllers
{
    [Area("Main")]
    public class DataController : Controller
    {
        private readonly DbstContext _context;
        public DataController(DbstContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = _context.Data.FirstOrDefaultAsync();
            return View(await data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeData([Bind("Street", "Number", "Locality", "Cp")] Data model)
        {
            if (ModelState.IsValid)
            {
                var data = await _context.Data.ToListAsync();
                if (data.Count > 0)
                {
                    foreach(var item in data)
                    {
                        _context.Data.Remove(item);
                    }
                    _context.SaveChanges();
                }
                _context.Data.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}