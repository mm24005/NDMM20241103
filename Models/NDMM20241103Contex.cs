using Microsoft.EntityFrameworkCore;

namespace NDMM20241103.Models
{
    public class NDMM20241103Contex : DbContext
    {
        public NDMM20241103Contex(DbContextOptions<NDMM20241103Contex> options): base(options)
        {
        }

        public virtual DbSet<DetFacturaVenta> DetFacturaVentas { get; set; } = null!;
        public virtual DbSet<FacturaVenta> FacturaVentas { get; set; } = null!;
    }
}
