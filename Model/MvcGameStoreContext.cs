using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class MvcGameStoreContext : DbContext
    {
        public MvcGameStoreContext(DbContextOptions<MvcGameStoreContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<GameModel> Game { get; set; }
        public DbSet<GenreModel> Genre { get; set; }
    }
}
