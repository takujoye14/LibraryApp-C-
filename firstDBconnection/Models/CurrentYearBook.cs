using firstDBconnection.Attributes;

namespace firstDBconnection.Models
{
    public class CurrentYearBook
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        [CurrentYear(ErrorMessage = "Published year cannot be greater than the current year.")]
        public int PublishedYear { get; set; }
    }
}