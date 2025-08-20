using exercise.webapi.Models;

namespace exercise.webapi.DTO_s
{
    public class AuthorGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<AuthorBookGet> Books { get; set; } = new List<AuthorBookGet>();

    }
}
