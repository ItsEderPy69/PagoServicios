using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class ApiDBContext : DbContext
    {

        //private const string connString = "User ID=local;Password=123456;Host=localhost;Port=9401;Database=Pagos;Pooling=true;Maximum Pool Size=1024;";
        public ApiDBContext(DbContextOptions options) : base(options)
        {
        }

        public ApiDBContext()
        {
        }
        private string? _conn  =null;
        public ApiDBContext(string conn)
        {
            _conn = conn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(connString);
            if (_conn != null) { optionsBuilder.UseNpgsql(_conn); };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }
        public DbSet<Servicios> Servicio { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<CuentaPagar> CuentaPagar { get; set; }
        public DbSet<PagoRealizado> PagoRealizado { get; set; }
    }
}
