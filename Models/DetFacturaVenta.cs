using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NDMM20241103.Models
{
    public class DetFacturaVenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public int IdFacturaVenta { get; set; }

        [Required(ErrorMessage = "Digite el nombre del producto")]
        public string Producto { get; set; } = null!;

        [Required(ErrorMessage = "Digite la cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Digite el precio unitario")]
        public decimal PrecioUnitario { get; set; }

        public virtual FacturaVenta IdFacturaVentaNavigation { get; set; } = null!;
    }
}
