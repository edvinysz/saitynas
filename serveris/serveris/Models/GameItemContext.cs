using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace serveris.Models
{
    public class GameItemContext : DbContext
    {
        public GameItemContext(DbContextOptions<GameItemContext> options) : base(options)
        {

        }

        public DbSet<GameItem> GameItems { get; set; }
    }
}
