using System;
using System.Linq;
using System.Threading.Tasks;
using MovieManager.Core;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using MovieManager.Persistence;

namespace MovieManager.ConsoleApp
{
    class Program
    {
        static async Task Main()
        {
            await InitDataAsync();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static async Task InitDataAsync()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");

            Console.WriteLine("Import der Movies und Categories in die Datenbank");
            await using IUnitOfWork unitOfWork = new UnitOfWork();
            Console.WriteLine("Datenbank löschen");
            await unitOfWork.DeleteDatabaseAsync();
            Console.WriteLine("Datenbank migrieren");
            await unitOfWork.MigrateDatabaseAsync();
            Console.WriteLine("Movies/Categories werden eingelesen");

            var movies = await ImportController.ReadFromCsvAsync();
            if (movies.Count == 0)
            {
                Console.WriteLine("!!! Es wurden keine Movies eingelesen");
                return;
            }

            // TODO: Store in database
            await unitOfWork.Movies.AddRangeAsync(movies);

            await unitOfWork.SaveChangesAsync();


            var countOfMovies = await unitOfWork.Movies.CountAsync();

            Console.WriteLine($"{countOfMovies} Entities wurden in DB gespeichert!");

            Console.WriteLine();
        }



    }
}
