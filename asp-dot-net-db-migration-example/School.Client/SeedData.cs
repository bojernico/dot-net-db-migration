using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using School.Core;
using School.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Client
{
    public static class SeedData
    {
        // Option 2
        public static bool Initialize(IServiceProvider serviceProvider) {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Pupils.Any())
            {
                return false;   // DB has been seeded
            }

            context.Pupils.AddRange(
                new Pupil { FirstName = "Nico", LastName = "Bojer" },
                new Pupil { FirstName = "Jonas", LastName = "Birklbauer" });
            context.SaveChanges();
            return true;
        }

    }
}
