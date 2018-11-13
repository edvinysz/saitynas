using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace serveris.Models
{
    public class LuckyNumberContext : DbContext
    {
        public LuckyNumberContext(DbContextOptions<LuckyNumberContext> options) : base(options)
        {

        }

        public DbSet<LuckyNumber> LuckyNumbers { get; set; }
    }
}
