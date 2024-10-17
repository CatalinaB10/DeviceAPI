using Microsoft.EntityFrameworkCore;
using DeviceAPI.Models;

namespace DeviceAPI.Context
{
    public class DeviceContext : DbContext
    {
        public DeviceContext(DbContextOptions<DeviceContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; } = null!;
        public DbSet<Device> Device { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}
