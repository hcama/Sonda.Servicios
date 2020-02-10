
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sonda.Core.Models;
using Sonda.Data.Security;

namespace Sonda.Data
{
   
   //public class AplicationDbContext : DbContext
    public class AplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }

    }
}
