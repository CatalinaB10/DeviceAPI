
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeviceAPI.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DeviceContext>
    {
        public DeviceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeviceContext>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var conn = configuration.GetConnectionString("DemoContext");

            optionsBuilder.UseNpgsql(conn);
            return new DeviceContext(optionsBuilder.Options);
        }
    }
}
