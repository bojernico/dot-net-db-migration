using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Core.DataTransferObjects
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public int NumberOfMovies { get; set; }
        public int TotalDuration { get; set; }
        public string AverageLength { get; set; }
    }
}
