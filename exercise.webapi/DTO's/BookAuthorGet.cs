using exercise.webapi.Models;
using System.Text.Json.Serialization;

namespace exercise.webapi.DTO_s
{
    public class BookAuthorGet
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public BookAuthorGet(Author author)
        {
            Id = author.Id;
            FirstName = author.FirstName;
            LastName = author.LastName;
            Email = author.Email;
        }
    }
}
