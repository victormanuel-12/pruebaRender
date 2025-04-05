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
using Microsoft.AspNetCore.Identity;
using proyectoTienda.Areas.Identity.Pages.Account; // Corrected namespace for ApplicationUser




namespace proyectoTienda.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;
       private readonly UserManager<IdentityUser> _userManager;

    public CatalogoController(ILogger<CatalogoController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
{
    _logger = logger;
    _context = context;
    _userManager = userManager;
}


        public IActionResult Index(int? categoriaId)
    {
        // Si se pasa un categoriaId, filtra los productos de esa categoría
        var productos = categoriaId == null 
            ? _context.Productos.ToList()  // Si no se pasa categoriaId, muestra todos los productos
            : _context.Productos.Where(p => p.IDCategoria == categoriaId).ToList();

        return View(productos); // Muestra los productos filtrados (o todos si no hay filtro)
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

            [HttpPost]
            public async Task<IActionResult> Detalles(int productoId, int cantidad)
            {
                // Validamos que la cantidad sea mayor que cero
                if (cantidad <= 0)
                {
                  TempData["Mensaje"] = "La cantidad debe ser mayor que cero.";
            return RedirectToAction("Detalles", new { id = productoId });

                }

                // Obtenemos el ID del usuario actual (userId)
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                  TempData["Mensaje"] = "Usuario No autenticado,inicia sesion porfavor";
            return RedirectToAction("Detalles", new { id = productoId });

                }

                // Verificamos si el usuario ya existe en la tabla Usuarios
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.ID == userId);

                if (usuarioExistente == null)
                {
                    // Si el usuario no existe, lo agregamos a la tabla Usuarios
                    var user = await _userManager.FindByIdAsync(userId);
                    var nuevoUsuario = new Usuario
                    {
                        ID = userId,
                        Nombre = user.UserName,
                        Email = user.Email,
                        
                    };

                    _context.Usuarios.Add(nuevoUsuario);
                    await _context.SaveChangesAsync();  // Guardamos el nuevo usuario
                }

                // Verificamos si el producto existe y tiene stock suficiente
                var producto = await _context.Productos.FindAsync(productoId);
                if (producto == null)
                {
                    return NotFound("Producto no encontrado.");
                }

                if (producto.Stock < cantidad)
                {
                    return BadRequest($"Stock insuficiente. Solo hay {producto.Stock} disponibles.");
                }

                // Buscamos un pedido pendiente (carrito) del usuario o creamos uno nuevo
                var pedidoPendiente = await _context.Pedidos
                    .Include(p => p.DetallesPedidos)
                    .FirstOrDefaultAsync(p => p.IDCliente == userId && p.Estado == 0);

                if (pedidoPendiente == null)
                {
                    // Creamos un nuevo pedido (carrito)
                    pedidoPendiente = new Pedido
                    {
                        IDCliente = userId, // Usamos el UserId para asociar el pedido al usuario
                        FechaPedido = DateTime.Now,
                        Estado = 0, // Pendiente
                        DetallesPedidos = new List<DetallePedido>()
                    };

                    _context.Pedidos.Add(pedidoPendiente);
                    await _context.SaveChangesAsync(); // Guardamos para obtener el ID generado
                }

                // Verificamos si el producto ya está en el carrito
                var detalleExistente = pedidoPendiente.DetallesPedidos?
                    .FirstOrDefault(d => d.IDProducto == productoId);

                if (detalleExistente != null)
                {
                    // Actualizamos la cantidad si ya existe
                    detalleExistente.Cantidad = cantidad;
                    detalleExistente.ActualizarSubtotal();
                }
                else
                {
                  

                  _logger.LogInformation("Agregando detalle: ProductoID={ProductoId}, Nombre={Nombre}, Precio={Precio}, Cantidad={Cantidad}, Subtotal={Subtotal}",
                producto.IDProducto,
                producto.Nombre,
                producto.Precio,
                cantidad,
                cantidad * producto.Precio // 👈 Aquí va el Subtotal
            );

                    // Agregamos un nuevo detalle al pedido
                    var nuevoDetalle = new DetallePedido
                    {
                      IDPedido = pedidoPendiente.IDPedido,
                      IDProducto = productoId,
                      Cantidad = cantidad,
                      Producto = producto, // Asignar para calcular el subtotal
                        Subtotal = cantidad * producto.Precio // Inicializamos el subtotal a 0
                    };



                    if (pedidoPendiente.DetallesPedidos == null)
                    {
                      pedidoPendiente.DetallesPedidos = new List<DetallePedido>();

                    }
                    pedidoPendiente.DetallesPedidos.Add(nuevoDetalle);
                }

                // Actualizamos los subtotales y guardamos los cambios
                pedidoPendiente.ActualizarSubtotales();
                
                await _context.SaveChangesAsync();
            /* _logger.LogInformation("Creando nuevo detalle de pedido:");
                    _logger.LogInformation("IDPedido: {IDPedido}, IDProducto: {IDProducto}, Cantidad: {Cantidad}, Subtotal: {Subtotal}********************************************"
                      ); */
                // Redirigimos al carrito o mostramos un mensaje
                TempData["Mensaje"] = "Producto agregado al carrito correctamente.";
                return RedirectToAction("Detalles", new { id = productoId });
            }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}