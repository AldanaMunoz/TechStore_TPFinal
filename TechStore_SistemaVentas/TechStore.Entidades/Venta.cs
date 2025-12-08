using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroFactura { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Required]
        public int VendedorId { get; set; }

        [ForeignKey("VendedorId")]
        public virtual Vendedor Vendedor { get; set; }

        [Required]
        public int SucursalId { get; set; }

        [ForeignKey("SucursalId")]
        public virtual Sucursal Sucursal { get; set; }

        public DateTime FechaVenta { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        public decimal PorcentajeDescuento { get; set; }

        public decimal MontoDescuento { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }

        [ForeignKey("MetodoPagoId")]
        public virtual MetodoPago MetodoPago { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; }

        public virtual ICollection<DetalleVenta> Detalles { get; set; }

        public Venta()
        {
            FechaVenta = DateTime.Now;
            Estado = "Completada";
            Subtotal = 0;
            PorcentajeDescuento = 0;
            MontoDescuento = 0;
            Total = 0;
            Detalles = new List<DetalleVenta>();
        }

        [NotMapped]
        public string ClienteRazonSocial => Cliente?.RazonSocial ?? string.Empty;

        [NotMapped]
        public string VendedorNombre => Vendedor?.NombreCompleto ?? string.Empty;

        [NotMapped]
        public string SucursalNombre => Sucursal?.Nombre ?? string.Empty;

        [NotMapped]
        public string MetodoPagoNombre => MetodoPago?.Nombre ?? string.Empty;
    }
}
