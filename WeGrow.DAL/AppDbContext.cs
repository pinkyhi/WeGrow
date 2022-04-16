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
            // Multi-field primary keys
            modelBuilder.Entity<Receipt>()
    .           HasKey(x => new { x.Order_Id, x.Module_Id });

            // Indexes
            modelBuilder.Entity<ModuleInstance>()
                .HasIndex(m => new { m.Module_Id, m.System_Id }).IsUnique();

            // One to many binding
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Receipts)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.Order_Id)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Module>()
                .HasMany(x => x.Receipts)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.Module_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Module>()
                .HasMany(x => x.ModuleInstances)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.Module_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SystemInstance>()
                .HasMany(x => x.ModuleInstances)
                .WithOne(x => x.System)
                .HasForeignKey(x => x.System_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SystemInstance>()
                .HasMany(x => x.Schedules)
                .WithOne(x => x.System)
                .HasForeignKey(x => x.System_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<SystemInstance>()
                .HasMany(x => x.Grows)
                .WithOne(x => x.System)
                .HasForeignKey(x => x.System_Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Schedule>()
                .HasMany(x => x.Grows)
                .WithOne(x => x.Schedule)
                .HasForeignKey(x => x.Schedule_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // Module propeties setting
            modelBuilder.Entity<Module>()
                .Property(x => x.Type)
                .HasConversion<int>();
            modelBuilder.Entity<Module>()
                .Property(x => x.Subject)
                .HasConversion<int>();
            modelBuilder.Entity<Module>()
                .Property(x => x.Price)
                .HasPrecision(8,2);

            // Order properties setting
            modelBuilder.Entity<Order>()
                .Property(x => x.Status)
                .HasConversion<int>();

            // Receipt properties setting
            modelBuilder.Entity<Receipt>()
                .Property(x => x.Cache_Price)
                .HasPrecision(8, 2);

            // Grow properties setting
            modelBuilder.Entity<Grow>()
                .Property(x => x.Status)
                .HasConversion<int>();
        }
    }
}
