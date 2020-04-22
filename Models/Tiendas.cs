using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VentasApp.Models
{
    public class Tiendas
    {
        
        [Required, Key, Display(Name = "Código")]
        public int Id_Tienda { get; set; }
        
        [Required, StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }
        
        [Required, StringLength(50, MinimumLength = 3)]
        public string Encargado { get; set; }
        
        [Required, DataType(DataType.PhoneNumber)]
        public int Contacto { get; set; }

        [Required, StringLength( 1000, MinimumLength = 5)]
        public string Direccion {get; set;}

        /*DOY*/
        public virtual ICollection<Ventas> Ventas {get; set;}
    }
}