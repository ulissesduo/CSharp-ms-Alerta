using Microsoft.EntityFrameworkCore;
using msAlerta.Entity;

namespace msAlerta.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options ) : base(options)
        {
            
        }

        public DbSet<Alerta> Alerta { get; set; }
    }
}
