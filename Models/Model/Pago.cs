using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoTienda.Models
{
  [Table("Pagos")]
    public class Pago
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IDPago { get; set; }

    [Required]
    public int IDPedido { get; set; }

    [Required]
    public int MetodoPago { get; set; } // 0=Tarjeta, 1=PayPal, 2=Transferencia

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Monto { get; set; }

    [Required]
    public DateTime FechaPago { get; set; } = DateTime.Now;

    // Propiedad de navegación
    [ForeignKey("IDPedido")]
    public Pedido? Pedido { get; set; }
  }
}