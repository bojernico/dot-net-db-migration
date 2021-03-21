using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieManager.Core.Entities
{
    public class Movie : EntityObject
    {
        public int Duration { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId  { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }


        public int Year { get; set; }

        public string Title { get; set; }

    }
}
