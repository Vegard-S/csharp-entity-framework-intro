using exercise.webapi.DTO_s;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("/books");
            books.MapGet("/", GetBooks);
            books.MapGet("/{id}", GetOneBook);
            books.MapPut("/{id}", UpdateBook);
            books.MapDelete("/{id}", DeleteBook);
            books.MapPost("/", CreateBook);


        }

        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var result = await bookRepository.GetAllBooks();
            
            List<Object> response = new List<Object>();

            foreach (var item in result)
            {
                BookGet book = new BookGet(item);
                response.Add(book);
            }

            return TypedResults.Ok(response);
            
        }

        private static async Task<IResult> GetOneBook(IBookRepository bookRepository, int id)
        {
            var result = await bookRepository.GetBook(id);

            BookGet book = new BookGet(result);

            return TypedResults.Ok(book);
        }

        private static async Task<IResult> UpdateBook(IBookRepository bookRepository, int bookId, int authorId)
        {
            var resultGet = await bookRepository.GetBook(bookId);
            var resultUpdate = await bookRepository.UpdateBook(resultGet, authorId);

            BookGet book = new BookGet(resultUpdate);
            return TypedResults.Ok(book);
        }

        private static async Task<IResult> DeleteBook(IBookRepository bookRepository, int id)
        {
            try
            {
                var model = await bookRepository.GetBook(id);
                if (await bookRepository.DeleteBook(id)) return Results.Ok(new { When = DateTime.Now, Status = "deleted", Title = model.Title });
                return TypedResults.NotFound();
            }
            catch (Exception ex)
            {

                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> CreateBook(IBookRepository bookRepository, BookPost model)
        {
            try
            {
                Author author = await bookRepository.GetAuthor(model.AuthorId);
                if (author != null)
                {


                    Book book = new Book()
                    {
                        Title = model.Title,
                        AuthorId = model.AuthorId,
                        Author = author
                    };
                    await bookRepository.CreateBook(book);

                    return TypedResults.Created($"https://localhost:7054/books/{book.Id}", book);
                }
                else
                {
                    return TypedResults.NotFound();
                }
            
            }
            catch (Exception)
            {

                return TypedResults.BadRequest();
            }
        }
    }
}
