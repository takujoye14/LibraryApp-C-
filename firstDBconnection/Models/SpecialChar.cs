using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class SpecialChar
    {
        public int Id { get; set; }

        [NoSpecialCharacters(ErrorMessage = "Title cannot contain special characters.")]
        public string Title { get; set; } = string.Empty;

        [NoSpecialCharacters(ErrorMessage = "Author cannot contain special characters.")]
        public string Author { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }
    }
}