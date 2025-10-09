using Microsoft.AspNetCore.Mvc;
using PraticaNv3Livraria.Communication.Request;
using PraticaNv3Livraria.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookInMemoryService _bookService;

    public BooksController(BookInMemoryService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Book>> Get()
    {
        return Ok(_bookService.GetAll());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Book> GetById(Guid id)
    {
        var book = _bookService.GetById(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Book> Post([FromBody] BookCreateRequest request)
    {
        if (_bookService.Exists(request.Title, request.Author))
        {
            return BadRequest(new { message = $"Um livro com o título '{request.Title}' e autor '{request.Author}' já existe." });
        }

        var bookToSave = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            Stock = request.Stock
        };

        var newBook = _bookService.Add(bookToSave);

        return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Put(Guid id, [FromBody] BookCreateRequest book)
    {

        var updatedBook = _bookService.Update(id, book);

        if (updatedBook == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(Guid id)
    {
        var deleted = _bookService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}