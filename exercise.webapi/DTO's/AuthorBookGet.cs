using exercise.webapi.Models;

namespace exercise.webapi.DTO_s
{
    public class AuthorBookGet
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }

    }
}
