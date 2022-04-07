using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeGrow.DAL.Entities;

namespace WeGrow.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .Property(x => x.Type)
                .HasConversion<int>();
            modelBuilder.Entity<Module>()
                .Property(x => x.Subject)
                .HasConversion<int>();
            modelBuilder.Entity<Module>()
                .Property(x => x.Price)
                .HasPrecision(8,2);
        }
    }
}
