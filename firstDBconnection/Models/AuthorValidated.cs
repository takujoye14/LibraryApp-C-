using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class AuthorValidated
    {
        public int Id { get; set; }

        [NoSpecialCharacters(ErrorMessage = "Name cannot contain special characters.")]
        [MaximumWords(3, ErrorMessage = "Name cannot exceed 3 words.")]
        public string Name { get; set; } = string.Empty;

        [NoSpecialCharacters(ErrorMessage = "Country cannot contain special characters.")]
        public string Country { get; set; } = string.Empty;

        public int BirthYear { get; set; }
    }
}