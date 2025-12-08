using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Entidades
{
    [Table("Vendedores")]
    public class Vendedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [Required]
        public int SucursalId { get; set; }

        [ForeignKey("SucursalId")]
        public virtual Sucursal Sucursal { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaIngreso { get; set; }

        [NotMapped]
        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }

        public Vendedor()
        {
            Activo = true;
            FechaIngreso = DateTime.Now;
        }
    }
}
