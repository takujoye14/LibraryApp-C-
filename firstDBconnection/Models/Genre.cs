using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstDBconnection.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}