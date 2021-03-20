using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Core;
using System;
using System.Diagnostics;

namespace School.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pupil> Pupils { get; set; }

        // Option 1 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pupil>()
                .HasData(new Pupil { Id=1, FirstName = "Nico", LastName = "Bojer" },
                         new Pupil { Id=2, FirstName = "Jonas", LastName = "Birklbauer" });
        }
    }
}
