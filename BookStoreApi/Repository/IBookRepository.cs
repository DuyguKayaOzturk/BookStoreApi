using BookStoreApi.Model;

namespace BookStoreApi.Repository;

public interface IBookRepository
{
    Book Add(Book book);
    Book? Update(int id, Book book);
    Book? Delete(int id);
    Book? GetById(int id);
    IEnumerable<Book> GetAll();
}
