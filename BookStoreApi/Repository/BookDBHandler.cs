using MySql.Data.MySqlClient;
using BookStoreApi.Model;


namespace BookStoreApi.Repository;

public class BookDbHandler : IBookRepository
{

    private readonly string? _connectionString;
    private readonly ILogger<BookDbHandler> _logger;

    
    public BookDbHandler(IConfiguration config, ILogger<BookDbHandler> logger)
    {

        _connectionString = config.GetConnectionString("Server=localhost; Port=3306; Database=BookStore; User ID=ga-app; Password=ga-5ecret-%");
        _connectionString = _connectionString?
            .Replace("{ga-app}", Environment.GetEnvironmentVariable("ga-app"))
            .Replace("{a-5ecret-%}", Environment.GetEnvironmentVariable("a-5ecret-%"));

        _logger = logger;
    }
    public Book Add(Book book)
    {
       

        _logger.LogDebug("Adds a new book in db with title: {@Book}", book);

        
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        
        MySqlCommand cmd = new("INSERT INTO Book (Title, Author, PublicationYear, ISBN, InStock) " +
        "values (@Title, @Author, @PublicationYear, @ISBN, @InStock)", conn);

        cmd.Parameters.AddWithValue("@Title", book.Title);
        cmd.Parameters.AddWithValue("@Author", book.Author);
        cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
        cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
        cmd.Parameters.AddWithValue("@InStock", book.InStock);

        var rowsAffected = cmd.ExecuteNonQuery();

        cmd.CommandText = "SELECT LAST_INSERT_ID()";
        var lastIdObject = cmd.ExecuteScalar();

        book.Id = Convert.ToInt64(lastIdObject);
        return book;

    }

    public Book? Delete(int id)
    {
        
        var bookToDelete = GetById(id);

        if (bookToDelete == null)
            return null;

        
        using MySqlConnection conn = new(_connectionString);
        
        conn.Open();

        
        MySqlCommand cmd = new("DELETE FROM Book WHERE Id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected == 0)
            return null;

        
        return bookToDelete;
    }

    public IEnumerable<Book> GetAll()
    {
        throw new Exception("We test with an exception");
        var bookList = new List<Book>();
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        MySqlCommand cmd = new("SELECT Id, Title, Author, PublicationYear, ISBN, InStock FROM Book", conn);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var book = new Book
            {
                Id = reader.GetInt32("Id"),
                PublicationYear = reader.GetInt32("PublicationYear"),
                Title = reader.GetString("Title"),
                Author = reader.GetString("Author"),
                ISBN = reader.GetString("ISBN"),
                InStock = reader.GetInt32("InStock")
            };
            bookList.Add(book);
        }

        return bookList;
    }

    public Book? GetById(int id)
    {
        throw new Exception("We test with getbyid an exception");
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        MySqlCommand cmd = new("SELECT Id, Title, Author, PublicationYear, ISBN, InStock FROM Book where id=@Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Book()
            {
                Id = reader.GetInt32("Id"),
                PublicationYear = reader.GetInt32("PublicationYear"),
                Title = reader.GetString("Title"),
                Author = reader.GetString("Author"),
                ISBN = reader.GetString("ISBN"),
                InStock = reader.GetInt32("InStock")
            };
        }
        return null;
    }

    public Book? Update(int id, Book book)
    {
        using MySqlConnection conn = new(_connectionString);
        conn.Open();

        MySqlCommand cmd = new(
            "UPDATE Book " +
            "SET Title=@Title, Author=@Author, PublicationYear=@PublicationYear, ISBN=@ISBN, InStock=@InStock " +
            "WHERE Id=@Id", conn);

        cmd.Parameters.AddWithValue("@Title", book.Title);
        cmd.Parameters.AddWithValue("@Author", book.Author);
        cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
        cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
        cmd.Parameters.AddWithValue("@InStock", book.InStock);
        cmd.Parameters.AddWithValue("@Id", id);

        var nrEffectedRows = cmd.ExecuteNonQuery();

        if (nrEffectedRows == 0)
            return null;

        book.Id = id;
        return book;

    }
}


