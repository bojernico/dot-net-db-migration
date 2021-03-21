using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieManager.Core.Contracts;
using MovieManager.Core.DataTransferObjects;
using MovieManager.Core.Entities;
using MovieManager.Persistence;

namespace MovieManager.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CategoriesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<ICollection<object>>> GetCategories()
            => Ok((await _uow.Categories.ToCollectionAsync())
                .Select(c => new
                {
                    c.CategoryName,
                    Movies = c.Movies
                    .Select(m => new
                    {
                        m.Title,
                        m.Duration,
                        m.Year
                    })
                    .ToList()
                })
                .ToList());

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetCategory(int id)
        {
            var category = await FindCategoryAsync(id);

            return category != null ? Ok(new
            {
                category.CategoryName,
                Movies = category.Movies.Select(m => new { m.Title, m.Duration, m.Year }).ToList()
            }) : NotFound();
        }

        private async Task<Category> FindCategoryAsync(int id) 
            => await _uow.Categories.FindAsync(id);

        // GET: api/Categories/4/movies
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<ICollection<object>>> GetMoviesForCategory(int id)
        {
            var category = await FindCategoryAsync(id);
            return category != null ? Ok(category.Movies.OrderBy(m => m.Title).Select(m => new { m.Title, m.Duration, m.Year }).ToList()) : NotFound();
        }

        // GET: api/Categories/mostmovies
        [HttpGet("mostmovies")]
        public async Task<ActionResult<CategoryDto>> GetCategoryWithMostMovies()
        {
            var category = await _uow.Categories.FindWithMostMoviesAsync();
            return category != null ? category : NotFound();

        }

        // GET: api/Categories/Action/yearwithmostpublications
        [HttpGet("{categoryName}/yearwithmostpublications")]
        public async Task<ActionResult<int>> GetYearWithMostPublicationsForCategory(string categoryName)
            => await _uow.Categories.FindYearWithMostPublicationsAsync(categoryName);

        // GET: api/Categories/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<ICollection<CategoryDto>>> GetStatistics()
            => Ok(await _uow.Categories.GetStatisticsAsync());

        // GET: api/Categories/averagelengths
        [HttpGet("averagelengths")]
        public async Task<ActionResult<ICollection<object>>> GetAverageMovieLengths() 
            => Ok((await _uow.Categories.GetAverageMovieLengthsAsync())
                .Select(dto => new
                {
                    dto.CategoryName,
                    dto.AverageLength
                })
                .ToList());

        /*// PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            //_context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _uow.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _uow.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
    }
}
