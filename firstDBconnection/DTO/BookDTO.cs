namespace firstDBconnection.DTO;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public AuthorDTO? Author { get; set; } 
}