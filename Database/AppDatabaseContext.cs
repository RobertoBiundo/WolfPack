using System.Collections.Generic;
using Database.Entities;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class AppDatabaseContext : DbContext
    {
        public DbSet<Wolf> Wolfs { get; set; }
        
        public DbSet<WolfPack> WolfPack { get; set; }
        public DbSet<Pack> Packs { get; set; }

        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Keys
            modelBuilder.Entity<WolfPack>()
                .HasKey(wolfPack => new { wolfPack.WolfId, wolfPack.PackId });
            
            // Relationships
            modelBuilder.Entity<WolfPack>()
                .HasOne(wolfPack => wolfPack.Wolf)
                .WithMany(wolf => wolf.WolfPack)
                .HasForeignKey(wolfPack => wolfPack.WolfId);  
            
            modelBuilder.Entity<WolfPack>()
                .HasOne(wolfPack => wolfPack.Pack)
                .WithMany(pack => pack.WolfPack)
                .HasForeignKey(wolfPack => wolfPack.PackId);
        }
    }
}