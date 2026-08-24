
using BookQuotesBackend.DTOs;
using BookQuotesBackend.Data;
using BookQuotesBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace BookQuotesBackend.Services;

public class BookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Book>> GetBooks(int userId)
    {
        return await _context.Books
        .Where(book => book.UserId == userId)
        .OrderBy(book => book.Title)
        .ToListAsync();
    }

    public async Task<Book?> GetBook(int bookId, int userId)
    {
        return await _context.Books
            .FirstOrDefaultAsync(book =>
                book.Id == bookId &&
                book.UserId == userId
            );
    }

    public async Task<Book?> AddBook(BookRequest request, int userId)
    {
        if (!TryParsePublicationDate(request.PublicationDate, out DateTime publicationDate))
        {
            Console.WriteLine($"Invalid publication date: {request.PublicationDate}");
            return null;
        }

        Book book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublicationDate = publicationDate,
            UserId = userId
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<Book?> UpdateBook(int bookId, BookRequest request, int userId)
    {
        Book? book = await _context.Books
            .FirstOrDefaultAsync(book => book.Id == bookId && book.UserId == userId);

        if (book == null)
        {
            return null;
        }

        if (!TryParsePublicationDate(request.PublicationDate, out DateTime publicationDate))
        {
            Console.WriteLine($"Invalid publication date: {request.PublicationDate}");
            return null;
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.PublicationDate = publicationDate;

        await _context.SaveChangesAsync();

        return book;
    }

    public async Task<bool> DeleteBook(int bookId, int userId)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId && book.UserId == userId);
        if (book == null)
        {
            return false;
        }
        _context.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool TryParsePublicationDate(
    string publicationDate,
    out DateTime parsePublicationDate)
    {
        return DateTime.TryParseExact(
            publicationDate,
            "yyyy.MM.dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out parsePublicationDate
        );
    }
}