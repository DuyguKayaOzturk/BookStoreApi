namespace BookStoreApi.Model;

public class Book
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }
    public string ISBN { get; set; }
    public int InStock { get; set; }

    public Book()
    {
        Author = "";

    }
}