using Microsoft.EntityFrameworkCore;

namespace DBConnect.Models
{
    public class ConnectToMysql:DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectToMysql(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var CS = _configuration.GetConnectionString("CTMYSQL");
            options.UseMySql(CS, ServerVersion.AutoDetect(CS));
        }

        public DbSet<Users> Users { set; get; }
    }
}
