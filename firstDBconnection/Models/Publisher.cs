using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstDBconnection.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new();
    }
}