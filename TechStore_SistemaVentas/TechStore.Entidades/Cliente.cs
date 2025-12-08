using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoClienteId { get; set; }

        [ForeignKey("TipoClienteId")]
        public virtual TipoCliente TipoCliente { get; set; }

        [Required]
        [MaxLength(200)]
        public string RazonSocial { get; set; }

        [MaxLength(13)]
        public string CUIT { get; set; }

        [MaxLength(200)]
        public string Direccion { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public decimal LimiteCredito { get; set; }

        public decimal SaldoActual { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public Cliente()
        {
            Activo = true;
            FechaRegistro = DateTime.Now;
            LimiteCredito = 0;
            SaldoActual = 0;
        }
    }
}
