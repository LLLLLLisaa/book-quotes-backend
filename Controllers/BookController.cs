using BookQuotesBackend.DTOs;
using BookQuotesBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookQuotesBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var books = await _bookService.GetBooks(userId);

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var book = await _bookService.AddBook(request, userId);

        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(
        int id,
        BookRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var success = await _bookService.UpdateBook(
            id,
            request,
            userId
        );

        if (!success)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Book updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var success = await _bookService.DeleteBook(
            id,
            userId
        );

        if (!success)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Book deleted successfully."
        });
    }
}