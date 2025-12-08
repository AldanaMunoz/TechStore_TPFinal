using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("CuentasCorrientes")]
    public class CuentaCorriente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        public int? VentaId { get; set; }

        [ForeignKey("VentaId")]
        public virtual Venta Venta { get; set; }

        [Required]
        [MaxLength(20)]
        public string TipoMovimiento { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public decimal SaldoAnterior { get; set; }

        [Required]
        public decimal SaldoNuevo { get; set; }

        public DateTime FechaMovimiento { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public CuentaCorriente()
        {
            FechaMovimiento = DateTime.Now;
        }
    }
}
