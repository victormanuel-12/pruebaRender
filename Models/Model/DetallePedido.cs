using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoTienda.Models
{
  [Table("DetallesPedidos")]
  public class DetallePedido
  {
    
    [Required]
    public int IDPedido { get; set; }

    
    [Required]
    public int IDProducto { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Cantidad { get; set; }

  
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Subtotal { get; set; }

    // Propiedades de navegación
    
    public Pedido? Pedido { get; set; }

   
    public Producto? Producto { get; set; }
    // Método para calcular el subtotal
    public decimal CalcularSubtotal()
    {
      if (Producto == null)
      {
        return 0; // O manejar este caso según los requisitos del negocio
      }
      return Cantidad * Producto.Precio; // Accediendo al precio desde la clase Producto
    }

    // Método para actualizar el valor de Subtotal
    public void ActualizarSubtotal()
    {
      Subtotal = CalcularSubtotal();
    }
  }
}