using Microsoft.EntityFrameworkCore;
using DeviceAPI.Models;
using UserAPI.Models;

namespace DeviceAPI.Context
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Device { get; set; } = default!;
        
    }
}
