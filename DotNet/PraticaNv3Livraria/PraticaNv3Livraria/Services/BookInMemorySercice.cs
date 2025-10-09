using PraticaNv3Livraria.Communication.Request;
using PraticaNv3Livraria.Models;

public class BookInMemoryService
{
    private readonly List<Book> _books = new List<Book>();

    public BookInMemoryService()
    {

        _books.Add(new Book
        {
            Id = Guid.NewGuid(),
            Title = "O Código Limpo",
            Author = "Robert C. Martin",
            Genre = "Programação",
            Price = 120.00m,
            Stock = 5
        });

        _books.Add(new Book
        {
            Id = Guid.NewGuid(),
            Title = "1984",
            Author = "George Orwell",
            Genre = "Ficção Distópica",
            Price = 45.50m,
            Stock = 12
        });
    }

    public List<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(Guid id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public bool Exists(string title, string author)
    {
        return _books.Any(b =>
            b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
            b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }

    public Book Add(Book newBook)
    {
        newBook.Id = Guid.NewGuid();

        _books.Add(newBook);

        return newBook;
    }

    public Book? Update(Guid id, BookCreateRequest updatedBook)
    {
        var existingBook = GetById(id);

        if (existingBook == null)
        {
            return null;
        }

        existingBook.Title = updatedBook.Title;
        existingBook.Author = updatedBook.Author;
        existingBook.Genre = updatedBook.Genre;
        existingBook.Price = updatedBook.Price;
        existingBook.Stock = updatedBook.Stock;

        return existingBook;
    }

    public bool Delete(Guid id)
    {
        var existingBook = GetById(id);

        if (existingBook == null)
        {
            return false;
        }

        return _books.Remove(existingBook);
    }
}