using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieManager.Core.DataTransferObjects;

namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;


        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> FindIdByNameAsync(string name)
            => (await _dbContext.Categories.SingleOrDefaultAsync(c => c.CategoryName == name)).Id;
        

        public async Task<int> CountAsync()
            => await _dbContext.Categories.CountAsync();

        public async Task<Category> FindAsync(int id)
            => await _dbContext.Categories
                .Include(c => c.Movies)
                .SingleOrDefaultAsync(c => c.Id == id);

        public async Task<CategoryDto> FindWithMostMoviesAsync()
            => await _dbContext.Categories
                .Include(c => c.Movies)
                .OrderByDescending(c => c.Movies.Count)
                .Select(c => new CategoryDto
                {
                    CategoryName = c.CategoryName,
                    NumberOfMovies = c.Movies.Count,
                    TotalDuration = c.Movies.Sum(m => m.Duration)
                })
                .FirstOrDefaultAsync();
        public async Task<int> FindYearWithMostPublicationsAsync(string categoryName)
            => (await _dbContext.Categories.Include(c => c.Movies).SingleAsync(c => c.CategoryName == categoryName))
                .Movies
                .GroupBy(p => p.Year)
                .OrderByDescending(p => p.Count())
                .FirstOrDefault()
                .Key;

        public async Task<ICollection<Category>> ToCollectionAsync()
            => await _dbContext.Categories
                .Include(c => c.Movies)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

        public async Task<ICollection<CategoryDto>> GetStatisticsAsync()
            => await _dbContext.Categories
                .Include(c => c.Movies)
                .OrderBy(c => c.CategoryName)
                .Select(c => new CategoryDto
                {
                    CategoryName = c.CategoryName,
                    NumberOfMovies = c.Movies.Count,
                    TotalDuration = c.Movies.Sum(m => m.Duration)
                })
                .ToListAsync();

        public async Task<ICollection<CategoryDto>> GetAverageMovieLengthsAsync()
        {
            var anonyms = await _dbContext.Categories
                .Include(c => c.Movies)
                .Select(c => new
                {
                    AverageMinutes = c.Movies.Sum(m => m.Duration) / (double)c.Movies.Count,
                    c.CategoryName
                })
                .ToListAsync();

            return anonyms
                .OrderByDescending(anonym => anonym.AverageMinutes)
                .Select(anonym => new CategoryDto
                {
                    AverageLength = Parse(anonym.AverageMinutes),
                    CategoryName = anonym.CategoryName
                })
                .ToList();

        }

        private static string Parse(double v)
         => TimeSpan.FromSeconds(v * 60).ToString(@"hh\ \h\ mm\ \m\i\n\ ss\ \s\e\c");
    }
}