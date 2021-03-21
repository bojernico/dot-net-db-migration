using System.Collections.Generic;
using System.Threading.Tasks;
using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;


namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        Task<int> CountAsync();
        Task<ICollection<Category>> ToCollectionAsync();
        Task<Category> FindAsync(int id);
        Task<CategoryDto> FindWithMostMoviesAsync();
        Task<int> FindYearWithMostPublicationsAsync(string categoryName);
        Task<ICollection<CategoryDto>> GetStatisticsAsync();
        Task<ICollection<CategoryDto>> GetAverageMovieLengthsAsync();
    }
}
