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
using proyectoTienda.ViewModel;

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
        .Select(c => new CategoriaViewModel
        {
            Categoria = c,
            ProductosCount = c.Productos.Count()
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
       public IActionResult AgregarCategoria()
{
    var categoria = new Categoria(); // Asegúrate de inicializar el modelo
    return View(categoria);
}


        [HttpPost]
public async Task<IActionResult> AgregarCategoria(Categoria nuevaCategoria)
{
    if (ModelState.IsValid)
    {
        // Validar que el nombre no exista ya (ignorando mayúsculas/minúsculas)
        bool existeCategoria = await _context.Categorias
            .AnyAsync(c => c.Nombre.ToLower() == nuevaCategoria.Nombre.ToLower());

        if (existeCategoria)
        {
            ModelState.AddModelError("Nombre", "Ya existe una categoría con ese nombre.");
            return View(nuevaCategoria); // Retorna la vista con los datos ingresados
        }

        try
        {
            _context.Categorias.Add(nuevaCategoria);
            await _context.SaveChangesAsync();
            return RedirectToAction("Categoria"); // o el nombre real de tu acción/lista
        }
        catch (Exception ex)
        {
            // Puedes tener una vista Error.cshtml que reciba un modelo personalizado si quieres
            return View("Error", new { message = ex.Message });
        }
    }

    return View(nuevaCategoria); // Retorna la vista con errores si ModelState es inválido
}

        
        [HttpPost]
public async Task<IActionResult> EliminarCategoria(int id)
{
    try
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        // Eliminar la categoría
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        // Redirigir a la vista de categorías después de eliminar
        return RedirectToAction("Categoria");
    }
    catch (Exception ex)
    {
        // En caso de error, puedes manejarlo como desees
        return View("Error", new { message = ex.Message });
    }
}


    [HttpGet]
    public async Task<IActionResult> EditarCategoria(int id)
    {
      // Buscar la categoría en la base de datos
      var categoria = await _context.Categorias
                                    .FirstOrDefaultAsync(c => c.IDCategoria == id);

      // Si no se encuentra la categoría, devolver una vista de error o redirigir a otro lugar
      if (categoria == null)
      {
        return NotFound();
      }

      // Crear el ViewModel para pasar los datos al formulario
      var viewModel = new EditarCategoriaViewModel
      {
        Id = categoria.IDCategoria,
        Nombre = categoria.Nombre,
        Descripcion = categoria.Descripcion
      };

      return View(viewModel);
    }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditarCategoria(EditarCategoriaViewModel model)
{
    if (ModelState.IsValid)
    {
        // Buscar la categoría por ID
        var categoria = await _context.Categorias.FindAsync(model.Id);

        if (categoria == null)
        {
            return NotFound();
        }

        // Actualizar los valores de la categoría
        categoria.Nombre = model.Nombre;
        categoria.Descripcion = model.Descripcion;

        // Guardar los cambios en la base de datos
        _context.Update(categoria);
        await _context.SaveChangesAsync();

        // Redirigir a la página de categorías
        return RedirectToAction("Categoria");
    }

    // Si el modelo no es válido, devolver la vista con el formulario
    return View(model);
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