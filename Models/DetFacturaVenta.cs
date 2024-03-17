namespace NDMM20241103.Models
{
    public class DetFacturaVenta
    {
        public int Id { get; set; }
        public int IdFacturaVenta { get; set; }
        public string Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public virtual FacturaVenta IdFacturaVentaNavigation { get; set; } = null!;
    }
}
