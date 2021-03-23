using Microsoft.EntityFrameworkCore.Migrations;
using MovieManager.Core;
using System.Linq;
using System;

namespace MovieManager.Persistence.Migrations
{
    public partial class SeedDatabaseMigration : Migration
    {
        protected override async void Up(MigrationBuilder migrationBuilder)
        {
            Console.WriteLine("Before inserting");

            var movies = await ImportController.ReadFromCsvAsync();
            var categoryNames = movies
                .Select(m => m.Category.CategoryName)
                .Distinct();

            foreach (var name in categoryNames)
            {
                migrationBuilder.InsertData(
                    table: "Categories",
                    column: "CategoryName",
                    value: name);
            }

            foreach (var movie in movies)
            {
                var index = movie.Title.IndexOf("'");

                var title = index != -1 ? movie.Title.Insert(index, "'") : movie.Title;

                string sql = ("insert into Movies (Duration, CategoryId, Year, Title) " +
                    "values (" + movie.Duration + "," +
                    " (select Id from Categories where CategoryName = '" + movie.Category.CategoryName + "'), " + movie.Year + ", '" + title + "')");
                migrationBuilder.Sql(sql);

            }
            Console.WriteLine("After inserting");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Movies");
            migrationBuilder.Sql("delete from Categories");
        }
    }
}
