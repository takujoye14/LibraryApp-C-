using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [NoSpecialCharacters(ErrorMessage = "Name cannot contain special characters.")]
        [MaximumWords(3, ErrorMessage = "Name cannot exceed 3 words.")]
        public string Name { get; set; } = string.Empty;

        [NoSpecialCharacters(ErrorMessage = "Country cannot contain special characters.")]
        public string Country { get; set; } = string.Empty;

        public int BirthYear { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}