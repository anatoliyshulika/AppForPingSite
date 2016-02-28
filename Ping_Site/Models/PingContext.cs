using System.Data.Entity;

namespace Ping_Site
{
    public class PingContext : DbContext
    {
        public DbSet<DataSite> DataSites { get; set; }
    }
}