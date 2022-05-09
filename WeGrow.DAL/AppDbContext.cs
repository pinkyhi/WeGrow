using Microsoft.EntityFrameworkCore;
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
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<SystemInstance>()
                .HasOne(x => x.Schedule)
                .WithMany(x => x.Systems)
                .HasForeignKey(x => x.ScheduleId)
                .OnDelete(DeleteBehavior.SetNull);
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
