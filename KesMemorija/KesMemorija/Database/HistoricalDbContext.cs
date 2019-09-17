using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KesMemorija.Database
{
    public class HistoricalDbContext : DbContext
    {
        public DbSet<HistoricalProperty> dbSet { get; set; }
    }
}
