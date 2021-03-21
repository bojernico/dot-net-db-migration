using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManager.Core.Contracts
{
    public interface IMovieRepository
    {
        Task AddAsync(Movie movieA);
        Task AddRangeAsync(ICollection<Movie> movies);
        Task<int> CountAsync();
        Task<ICollection<Movie>> ToListAsync();
        Task<Movie> FindAsync(int id);
        Task<MovieDto> FindLongestAsync();
    }
}
