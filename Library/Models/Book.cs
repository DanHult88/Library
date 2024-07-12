using Library.Models;

public class Book
{
    public int BookID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool? IsAvailable { get; set; }

    // Nullable foreign key for Genre
    public int? GenreID { get; set; }
    public Genre Genre { get; set; }
}
