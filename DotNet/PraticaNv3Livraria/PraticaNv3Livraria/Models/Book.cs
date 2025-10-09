
namespace PraticaNv3Livraria.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.Empty;

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Genre { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
