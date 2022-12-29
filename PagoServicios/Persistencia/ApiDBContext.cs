using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class ApiDBContext : DbContext
    {

        private const string connString = "User ID=local;Password=123456;Host=localhost;Port=9401;Database=Pagos;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
        public ApiDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
