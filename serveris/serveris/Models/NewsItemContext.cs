using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace serveris.Models
{
    public class NewsItemContext : DbContext
    {
        public NewsItemContext(DbContextOptions<NewsItemContext> options) : base(options)
        {

        }

        public DbSet<NewsItem> NewsItems { get; set; }
    }
}
