using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace serveris.Models
{
    public class BetItemContext : DbContext
    {
        public BetItemContext(DbContextOptions<BetItemContext> options) : base(options)
        {

        }

        public DbSet<GameItem> GameItems { get; set; }
        public DbSet<BetItem> betItems { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
    }
}
