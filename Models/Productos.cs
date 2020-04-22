using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasApp.Models
{
    public class Productos
    {
        [Required, Key, Display(Name = "Código")]
        public int Id_Producto { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }
        
        [Required, Range(0, Double.PositiveInfinity)]
        public double Precio { get; set; }

        [Required, Range(0, Double.PositiveInfinity)]
        public int Cantidad { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Fecha_Vencimiento {get; set;}

        /*DOY*/
        public virtual ICollection<DetalleVentas> Detalle {get; set;}

        /*RECIBO*/
        [ForeignKey("Categorias"), Required]
        public int Id_Categoria { get; set; }
        public virtual Categorias Categoria { get; set; }
    }
}