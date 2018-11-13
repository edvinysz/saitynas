using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace serveris.Models
{
    public class serverisContext : DbContext
    {
        public serverisContext(DbContextOptions<serverisContext> options) : base(options)
        {

        }

        public DbSet<serverisItem> serverisItems { get; set; }
    }
}
