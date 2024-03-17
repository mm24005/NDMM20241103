namespace NDMM20241103.Models
{
    public class FacturaVenta
    {
        public FacturaVenta()
        {
            DetFacturaVenta = new List<DetFacturaVenta>();
        }

        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Correlativo { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public decimal TotalVenta { get; set; }

        public virtual IList<DetFacturaVenta> DetFacturaVenta { get; set; }
    }
}
