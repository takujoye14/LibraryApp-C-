using System.ComponentModel.DataAnnotations;
using firstDBconnection.Attributes;
using System.Collections.Generic;

namespace firstDBconnection.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}