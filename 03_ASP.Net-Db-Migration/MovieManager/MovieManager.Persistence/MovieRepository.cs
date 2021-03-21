using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MovieManager.Core.DataTransferObjects;
using System;

namespace MovieManager.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountAsync()
            => await _dbContext.Movies.CountAsync();

        public async Task AddAsync(Movie movie)
            => await _dbContext.Movies.AddAsync(movie);

        public async Task AddRangeAsync(ICollection<Movie> movies)
            => await _dbContext.Movies.AddRangeAsync(movies);

        public async Task<ICollection<Movie>> ToListAsync()
            => await _dbContext.Movies.ToListAsync();

        public async Task<Movie> FindAsync(int id)
             => await _dbContext.Movies.SingleOrDefaultAsync(c => c.Id == id);

        public async Task<MovieDto> FindLongestAsync()
            => await _dbContext.Movies
                .OrderByDescending(m => m.Duration)
                    .ThenBy(m => m.Title)
                .Select(m => new MovieDto
                {
                    Title = m.Title,
                    Duration = $"{m.Duration / 60:D2} h {m.Duration % 60:D2} min"
                })
                .FirstOrDefaultAsync();
    }
}