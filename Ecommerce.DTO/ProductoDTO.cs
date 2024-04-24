using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una descripcion")]
        public string? Descripcion { get; set; }
        
        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        public decimal? Precio { get; set; }
        [Required(ErrorMessage = "Ingrese una cantidad")]
        public int? Cantidad { get; set; }
        [Required(ErrorMessage = "Ingrese una imagen")]
        public string? Imagen { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public virtual CategoriaDTO? IdCategoriaNavigation { get; set; }
    }
}