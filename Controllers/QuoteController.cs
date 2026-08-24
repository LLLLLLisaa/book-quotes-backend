using BookQuotesBackend.DTOs;
using BookQuotesBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookQuotesBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuotesController : ControllerBase
{
    private readonly QuoteService _quoteService;

    public QuotesController(QuoteService quoteService)
    {
        _quoteService = quoteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuotes()
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var quotes = await _quoteService.GetQuotes(userId);

        return Ok(quotes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuote(int id)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var quote = await _quoteService.GetQuote(id, userId);

        if (quote == null)
        {
            return NotFound();
        }

        return Ok(quote);
    }

    [HttpPost]
    public async Task<IActionResult> AddQuote(QuoteRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var quote = await _quoteService.AddQuote(request, userId);

        if (quote == null)
        {
            return BadRequest(new
            {
                message = "Something went wrong."
            });
        }

        return Ok(quote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuote(
        int id,
        QuoteRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var quote = await _quoteService.UpdateQuote(
            id,
            request,
            userId
        );

        if (quote == null)
{
            return BadRequest(new
            {
                message = "Something went wrong."
            });
}

        return Ok(new
        {
            message = "Quote updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuote(int id)
    {
        var userId = int.Parse(User.FindFirst("id")!.Value);

        var success = await _quoteService.DeleteQuote(
            id,
            userId
        );

        if (!success)
        {
            return NotFound();
        }

        return Ok(new
        {
            message = "Quote deleted successfully."
        });
    }
}