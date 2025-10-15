using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class MaxWord
    {
        public int Id { get; set; }

        [MaximumWords(4, ErrorMessage = "Title cannot exceed 4 words.")]
        public string Title { get; set; } = string.Empty;

        [MaximumWords(2, ErrorMessage = "Author name cannot exceed 2 words.")]
        public string Author { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }
    }
}