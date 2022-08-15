using Microsoft.EntityFrameworkCore;

namespace DBConnect.Models
{
    public class ConnectToMssql : DbContext
    {
        public ConnectToMssql(DbContextOptions<ConnectToMssql>options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
