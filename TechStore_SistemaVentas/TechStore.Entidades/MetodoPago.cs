using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("MetodosPago")]
    public class MetodoPago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public bool RequiereValidacion { get; set; }

        public bool Activo { get; set; }

        public MetodoPago()
        {
            RequiereValidacion = false;
            Activo = true;
        }
    }
}
