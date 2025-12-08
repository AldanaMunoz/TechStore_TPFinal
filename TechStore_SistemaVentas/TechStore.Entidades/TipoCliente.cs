using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("TiposCliente")]
    public class TipoCliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public decimal PorcentajeDescuento { get; set; }

        public decimal MontoMinimoCompra { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public TipoCliente()
        {
            PorcentajeDescuento = 0;
            MontoMinimoCompra = 0;
        }
    }
}
