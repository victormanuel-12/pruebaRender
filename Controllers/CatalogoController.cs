using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proyectoTienda.Data;
using proyectoTienda.Models;
using Microsoft.EntityFrameworkCore;
using proyectoTienda.ViewModel;

namespace proyectoTienda.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        public CatalogoController(ILogger<CatalogoController> logger,  ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            return View("Index", productos);
        }

        public async Task<IActionResult> Detalles (int? id)
        {

            if (id == null)
            {
                return NotFound(); // Devuelve NotFound si no se proporciona el id
            }

            Producto objProduct = await _context.Productos.FindAsync(id);

            if (objProduct == null)
            {
                return NotFound(); // Devuelve NotFound si el producto con el id no existe
            }

            var similarProductos = await _context.Productos
                                          .Where(p => p.IDProducto != id && p.IDCategoria == objProduct.IDCategoria) // Excluir el producto actual
                                          .Take(4) // Limitar a 4 productos
                                          .ToListAsync();

            var viewModel = new ProductoDetalleViewModel
            {
                ProductoPrincipal = objProduct,
                ProductosSimilares = similarProductos
            };

            return View(viewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}