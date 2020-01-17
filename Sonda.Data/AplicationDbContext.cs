

using Microsoft.EntityFrameworkCore;
using Sonda.Core.Models;

namespace Sonda.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }

    }
}
