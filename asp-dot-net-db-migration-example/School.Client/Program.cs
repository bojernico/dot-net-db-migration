using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.Extensions.DependencyInjection;
using School.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace School.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Console.Clear();
                    Console.WriteLine(SeedData.Initialize(services));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddDbContext<ApplicationDbContext>(options =>
                   {
                       options.UseSqlServer(hostContext.Configuration.GetConnectionString("SqlServerConnection"));
                   });
               });
    }
}
