using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using proyectoTienda.Models.ENUM;
using proyectoTienda.Models;
using Microsoft.AspNetCore.Identity; // Corrected namespace for IdentityUser

namespace  proyectoTienda.Models
{
  [Table("Usuarios")]
    public class Usuario:IdentityUser
  {
    

    
    public TipoUsuario TipoUsuario { get; set; } // 0 = Cliente, 1 = Admin
                                                 // Enum para representar los tipos de usuario


    public ICollection<Pedido>? Pedidos { get; set; }
  }
}