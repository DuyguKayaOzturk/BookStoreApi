using BookStoreApi.Model;

namespace BookStoreApi.Repository;

public class BookInMemoryDb : IBookRepository
{
    private List<Book> _books = new();
    private int _lastId = 0;

    public Book Add(Book book)
    {
        _lastId++;
        book.Id = _lastId;
        _books.Add(book);
        return book;
    }

    public Book? Delete(int id)
    {
        var bookToDelete = _books.FirstOrDefault(p => p.Id == id);
        if (bookToDelete != null)
        {
            _books.Remove(bookToDelete);
            return bookToDelete;
        }
        return null;
    }

    public IEnumerable<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(int id)
    {
        var book = _books.FirstOrDefault(p => p.Id == id);
        if (book != null)
            return book;
        return null;
    }

    public Book? Update(int id, Book book)
    {
        // finne book i repo
        var bookToUpdate = _books.FirstOrDefault(p => p.Id == id);
        if (bookToUpdate != null)
        {
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.PublicationYear = book.PublicationYear;
            bookToUpdate.ISBN = book.ISBN;
            bookToUpdate.InStock = book.InStock;

            return bookToUpdate;
        }
        return null;
    }
}