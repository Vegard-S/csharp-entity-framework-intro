using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();

        public Task<Book> GetBook(int id);

        public Task<Book> UpdateBook(Book book, int authorId);

        public Task<bool> DeleteBook(int id);

        public Task<Book> CreateBook(Book book);

        public Task<Author> GetAuthor(int id);

    }
}
