using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VentasApp.Models
{
    public class Categorias
    {
        
        [Required, Key, Display(Name = "Código")]
        public int Id_Categoria { get; set; }

        [Required, StringLength( 1000, MinimumLength = 3)]
        public string Nombre { get; set; }

        /*DOY*/
        public virtual ICollection<Productos> Productos {get; set;}

    }
}