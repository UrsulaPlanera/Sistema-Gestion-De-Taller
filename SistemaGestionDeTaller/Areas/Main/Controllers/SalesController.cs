using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using SistemaGestionDeTaller.Areas.Main.Models;
using SistemaGestionDeTaller.Areas.Main.Models.ViewModel;
using SistemaGestionDeTaller.Models;

namespace SistemaGestionDeTaller.Areas.Main.Controllers
{
    [Area("Main")]
    public class SalesController : Controller
    {
        private readonly DbstContext _context;

        public SalesController(DbstContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime date)
        {
            IQueryable<Sale> search = _context.Sales;
            if (date.Date.Year > 2000)
            {
                search = _context.Sales.Where(s => s.Date.Date == date);
            }
            return View(await search.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var resultSale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
                var resultConcept = await (from concept in _context.Concepts
                                           join idsale in _context.Sales on concept.SaleNumber equals idsale.Id where id == concept.SaleNumber
                                           select new
                                           {
                                               Id = concept.Id,
                                               SaleNumber = concept.SaleNumber,
                                               NameProduct = concept.NameProduct,
                                               UnitPrice = concept.UnitPrice,
                                               Units = concept.Units,
                                               TotalPrice = concept.TotalPrice,
                                               SaleNumberNavigation = concept.SaleNumberNavigation,
                                           }).ToListAsync();

                if (resultConcept != null && resultSale != null)
                {
                    foreach (var c in resultConcept)
                    {
                        Concept aux = new Concept
                        {
                            Id = c.Id,
                            SaleNumber = c.SaleNumber,
                            NameProduct = c.NameProduct,
                            UnitPrice = c.UnitPrice,
                            Units = c.Units,
                            TotalPrice = c.TotalPrice,
                            SaleNumberNavigation = c.SaleNumberNavigation,
                        };
                        resultSale.Concepts.Add(aux);
                    }
                }
                return View(resultSale);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var conceptList = await _context.Concepts.Where(c => c.SaleNumber == id).ToListAsync();

            if (conceptList != null)
            {
                foreach (var concept in conceptList)
                {
                    _context.Concepts.Remove(concept);
                }
            }

            var saleItem = await _context.Sales.FindAsync(id);

            if (saleItem != null)
            {
                _context.Sales.Remove(saleItem);

            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //vista de pagina de venta
        public async Task<IActionResult> AddCart()
        {
            return View(await _context.Products.ToListAsync());
        }

        //añadir producto al carrito
        [HttpPost]
        public async Task<IActionResult> AddCart(int product, int unit)
        {
            var listProductsOnCart = await _context.Cart.ToListAsync();
            var exist = false;

            foreach (var p in listProductsOnCart)
            {
                if(p.Product == product)
                {
                    exist = true;
                }
            }

            if (exist)
            {
                var productExist = await _context.Cart.FirstOrDefaultAsync(c => c.Product == product);
                productExist.Units += unit;
            }
            else
            {
                Cart cart = new Cart
                {
                    Product = product,
                    Units = unit
                };

                _context.Cart.Add(cart);
            }

            _context.SaveChanges();

            return View(await _context.Products.ToListAsync());
        }

        //vista de productos en el carrito para confirmar la venta
        public async Task<IActionResult> Cart()
        {
            var productsInCart = await _context.Cart.ToListAsync();

            var list = new List<ProductCart>();

            foreach (var productCart in productsInCart)
            {
                var aux = await _context.Products.FirstOrDefaultAsync(p => p.Id == productCart.Product);
                if (aux != null)
                {
                    var newProduct = new ProductCart
                    {
                        IdCart = productCart.Id,
                        NameProduct = aux.Name,
                        Price = aux.SalePrice,
                        Units = productCart.Units
                    };
                    list.Add(newProduct);
                }
            }

            return View(list);
        }

        //remover producto del carrito de ventas
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var aux = await _context.Cart.FirstOrDefaultAsync(c => c.Id == id);
            if (aux != null)
            {
                _context.Cart.Remove(aux);
                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        //confirmar venta, vaciar carrito y volver a la pagina de venta
        [HttpPost]
        public async Task<IActionResult> ConfirmCart()
        {
            var sale = new Sale
            {
                Date = DateTime.Now,
                TotalPrice = 0,
            };

            _context.Sales.Add(sale);
            _context.SaveChanges();

            var lastSale = await _context.Sales.ToListAsync();
            var saleUse = lastSale[lastSale.Count-1];

            var productsInCart = await _context.Cart.ToListAsync();

            var totalPriceSale = new decimal();

            foreach(var productCart in productsInCart)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productCart.Product);

                var concept = new Concept
                {
                    SaleNumber = saleUse.Id,
                    NameProduct = product.Name,
                    UnitPrice = product.SalePrice,
                    Units = productCart.Units
                };

                product.Stock -= productCart.Units;

                totalPriceSale += (product.SalePrice * productCart.Units);

                _context.Concepts.Add(concept);
            }

            saleUse.TotalPrice = totalPriceSale;

            foreach(var product in productsInCart)
            {
                _context.Cart.Remove(product);
            }

            _context.SaveChanges();

            return View("Index", await _context.Sales.ToListAsync());
        }
    }
}
