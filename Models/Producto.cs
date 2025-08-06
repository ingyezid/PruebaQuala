using PruebaQuala.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PruebaQuala.Models
{
    public class Producto
    {
        [Key]
        public int CodigoProducto { get; set; }

        [Required (ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]    
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La referencia interna del producto es obligatoria.")]
        public string ReferenciaInterna { get; set; }
        
        [Required(ErrorMessage = "El precio unitario del producto es obligatorio.")]
        [Range(0.0000000001, double.MaxValue, ErrorMessage = "El precio unitario debe ser un valor positivo.")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "El estado del producto es obligatorio.")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "La unidad de medida del producto es obligatoria.")]
        public  string UnidadMedida { get; set; }

        [Required(ErrorMessage = "La fecha de creación del producto es obligatoria.")]
        [FechaPosterior(ErrorMessage = "La fecha de creación debe ser posterior a la fecha actual.")]
        public DateTime FechaCreacion { get; set; }

    }
}
