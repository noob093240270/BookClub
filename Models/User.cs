using Microsoft.Extensions.Primitives;

namespace BookClub.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Book> ReadBooks { get; set; } 
    }
}
