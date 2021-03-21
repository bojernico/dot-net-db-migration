using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MovieManager.Core.Entities;
using Utils;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "Movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static async Task<ICollection<Movie>> ReadFromCsvAsync()
        {

            var matrix = await MyFile.ReadStringMatrixFromCsvAsync(Filename, true);
            var categories = matrix.Select(cols => cols[2])
                .Distinct()
                .Select(title => new Category
                {
                    CategoryName = title.ToString()
                })
                .ToList();

            var movies = matrix.Select(line => new Movie
            {
                Title = line[0].ToString(),
                Year = int.Parse(line.GetValue(1).ToString()),
                Category = categories.SingleOrDefault(c => c.CategoryName == line[2].ToString()),
                Duration = int.Parse(line.GetValue(3).ToString())
            })
             .ToList();

            Console.WriteLine($"  Es wurden {movies.Count} Movies in {categories.Count} Kategorien eingelesen!");
            return movies;

        }

    }
}
