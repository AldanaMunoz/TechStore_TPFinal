using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Entidades
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        // Expose category name for binding in UI
        [NotMapped]
        public string NombreCategoria
        {
            get { return Categoria != null ? Categoria.Nombre : string.Empty; }
        }

        [Required]
        public decimal PrecioUnitario { get; set; }

        public int StockMinimo { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Producto()
        {
            Activo = true;
            FechaCreacion = DateTime.Now;
            StockMinimo = 10;
        }
    }
}
