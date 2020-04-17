using System;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using RestWebApplication.Data.Domain;

namespace RestWebApplication.Data
{
    public class RestWebAppDbContext:DbContext
    {

        public DbSet<Teacher> Teachers { get; set; }
        
        public RestWebAppDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);
        }
    }
    
}