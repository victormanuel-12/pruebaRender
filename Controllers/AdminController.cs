using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using proyectoTienda.Data;
using proyectoTienda.Models;

namespace proyectoTienda.Controllers
{
    
    public class AdminController : Controller
    {
          private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor con la inyección de dependencias para el DbContext
        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult HomeAdmin()
        {
            return View();
        }
        // Categoria
        [HttpGet]
            public async Task<IActionResult> Categoria()
            {
                try
                {
                    var categorias = await _context.Categorias
                        .Select(c => new 
                        {
                            Categoria = c,
                            ProductosCount = c.Productos.Count()  // Contar productos por cada categoría
                        })
                        .ToListAsync();

                    // Verificar si no hay categorías
                    if (categorias == null || categorias.Count == 0)
                    {
                        ViewBag.Message = "No hay categorías disponibles en el sistema.";
                    }

                    return View(categorias);
                }
                catch (Exception ex)
                {
                    // En caso de error, mostrar mensaje de error
                    return View("Error", new { message = ex.Message });
                }
            }


         // Agregar Categoria
        [HttpGet]
        public IActionResult AgregarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarCategoria(Categoria nuevaCategoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categorias.Add(nuevaCategoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Categoria");
                }
                catch (Exception ex)
                {
                    return View("Error", new { message = ex.Message });
                }
            }
            return View(nuevaCategoria);
        }

        
        // Productos
          // Productos
/* [HttpGet]
public async Task<IActionResult> Productos(int page = 1, int pageSize = 10)
{
    try
    {
        // Calcular el número de productos a omitir
        var skip = (page - 1) * pageSize;

        // Obtener los productos con paginación
        var productos = await _context.Productos
                                       .Skip(skip)   // Omitir productos de las páginas anteriores
                                       .Take(pageSize)  // Tomar solo los productos de la página actual
                                       .ToListAsync();

        // Verificar si no hay productos
        if (productos == null || productos.Count == 0)
        {
            ViewBag.Message = "No hay productos disponibles en el sistema.";
        }

        // Obtener el total de productos para calcular la cantidad de páginas
        var totalProductos = await _context.Productos.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalProductos / pageSize);

        // Crear el modelo de vista
        var model = new PaginatedList<Producto>
        {
            Items = productos,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize
        };

        return View(model);
    }
    catch (Exception ex)
    {
        return View("Error", new { message = ex.Message });
    }
} */



        [HttpGet]
        public IActionResult AgregarProducto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(Producto nuevoProducto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Productos.Add(nuevoProducto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Productos");
                }
                catch (Exception ex)
                {
                    return View("Error", new { message = ex.Message });
                }
            }
            return View(nuevoProducto);
        }

         // Lista de Pedidos
        [HttpGet]
        public async Task<IActionResult> ListaOrdenes()
        {
            try
            {
                var ordenes = await _context.Pedidos.ToListAsync();
                return View(ordenes);
            }
            catch (Exception ex)
            {
                return View("Error", new { message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}