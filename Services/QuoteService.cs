using BookQuotesBackend.DTOs;
using BookQuotesBackend.Data;
using BookQuotesBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesBackend.Services;

public class QuoteService
{
    private readonly AppDbContext _context;

    public QuoteService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<List<Quote>> GetQuotes(int userId)
    {
        return await _context.Quotes
        .Where(quote => quote.UserId == userId)
        .OrderBy(quote => quote.Text)
        .ToListAsync();

    }

    public async Task<Quote?> GetQuote(int quoteId, int userId)
    {
        return await _context.Quotes
            .FirstOrDefaultAsync(quote =>
                quote.Id == quoteId &&
                quote.UserId == userId
            );
    }

    public async Task<Quote> AddQuote(QuoteRequest request, int userId)
    {
        Quote quote = new Quote
        {
            Text = request.Text,
            Source = request.Source,
            UserId = userId
        };

        _context.Quotes.Add(quote);
        await _context.SaveChangesAsync();

        return quote;
    }

    public async Task<Quote?> UpdateQuote(int quoteId, QuoteRequest request, int userId)
    {
        Quote? quote = await _context.Quotes
            .FirstOrDefaultAsync(quote =>
                quote.Id == quoteId &&
                quote.UserId == userId
            );
            
        if (quote == null)
        {
            return null;
        }

        quote.Text = request.Text;
        quote.Source = request.Source;

        await _context.SaveChangesAsync();

        return quote;
    }
    
    public async Task<bool> DeleteQuote(int quoteId, int userId)
    {
        Quote? quote = await _context.Quotes
            .FirstOrDefaultAsync(quote =>
                quote.Id == quoteId &&
                quote.UserId == userId
            );

        if (quote == null)
        {
            return false;
        }
        _context.Quotes.Remove(quote);
        await _context.SaveChangesAsync();
        return true;
    }
}
