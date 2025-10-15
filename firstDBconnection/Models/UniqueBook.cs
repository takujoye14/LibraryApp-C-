using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class UniqueBook
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        [NotEqualTo("Title", ErrorMessage = "ISBN cannot be the same as Title.")]
        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }
    }
}