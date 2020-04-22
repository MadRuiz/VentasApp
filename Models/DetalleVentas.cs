using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasApp.Models
{
    public class DetalleVentas
    {
        
        [Required, Key, Display(Name = "Código")]
        public int Id_DetalleVenta { get; set; }

        [Required, Range(0, Double.PositiveInfinity)]
        public double Subtotal { get; set; }
        
        [Required, Range(0, Double.PositiveInfinity)]
        public int Cantidad { get; set; }


        /*RECIBO*/
        [ForeignKey("Ventas"), Required]
        public int Id_Venta { get; set; }
        public virtual Ventas Venta { get; set; }

        /*RECIBO*/
        [ForeignKey("Productos"), Required]
        public int Id_Producto { get; set; }
        public virtual Productos Producto { get; set; }
    }
}