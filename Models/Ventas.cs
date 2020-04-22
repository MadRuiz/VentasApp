using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasApp.Models
{
    public class Ventas
    {
        [Required, Key, Display(Name = "Código")]
        public int Id_Venta { get; set; }

        [Required, StringLength(50, MinimumLength = 4), Display(Name = "Código de Factura")]
        public string Codigo_Factura { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Nombre_Cliente { get; set; }

        [Required, DataType(DataType.Date)]
        public string Fecha { get; set; }
        
        [Required, Range(0, Double.PositiveInfinity)]
        public double Total { get; set; }
        /*DOY*/
        public virtual ICollection<DetalleVentas> Detalle {get; set;}
        /*RECIBO*/
        [ForeignKey("Tiendas"), Required]
        public int Id_Tienda { get; set; }
        public virtual Tiendas Tienda { get; set; }
    }
}