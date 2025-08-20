using exercise.webapi.Models;

namespace exercise.webapi.DTO_s
{
    public class BookGet
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public BookAuthorGet Author { get; set; }

        public BookGet(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            AuthorId = book.AuthorId;
            Author = new BookAuthorGet(book.Author); 
        }
    }
}
