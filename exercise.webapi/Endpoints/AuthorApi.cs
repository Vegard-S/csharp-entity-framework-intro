using exercise.webapi.DTO_s;
using exercise.webapi.Repository;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorsApi(this WebApplication app)
        {
            var author = app.MapGroup("/author");
            author.MapGet("/", GetAuthor);
            author.MapGet("/{id}", GetOneAuthor);
        }

        private static async Task<IResult> GetAuthor(IAuthorRepository authorRepository)
        {
            var result = await authorRepository.GetAllAuthors();

            List<Object> response = new List<Object>();

            foreach (var item in result)
            {
                var resultBooks = item.Books;
                List<AuthorBookGet> books = new List<AuthorBookGet>();
                foreach (var thing in resultBooks)
                {
                    AuthorBookGet book = new AuthorBookGet()
                    {
                        Id = thing.Id,
                        Title = thing.Title,
                        AuthorId = thing.AuthorId,
                    };
                    books.Add(book);
                    
                }
                AuthorGet author = new AuthorGet()
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Books = books

                };
                response.Add(author);
            }

            return TypedResults.Ok(response);

        }

        private static async Task<IResult> GetOneAuthor(IAuthorRepository authorRepository, int id)
        {
            var result = await authorRepository.GetAuthor(id);
            List<AuthorBookGet> books = new List<AuthorBookGet>();
            foreach (var item in result.Books)
            {
                AuthorBookGet book = new AuthorBookGet()
                {
                    Id = item.Id,
                    Title = item.Title,
                    AuthorId = item.AuthorId,
                };
                books.Add(book);
            }

            AuthorGet author = new AuthorGet()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Books = books
            };

            return TypedResults.Ok(author);
        }
    }
}
