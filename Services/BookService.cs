
using BookQuotesBackend.DTOs;
using BookQuotesBackend.Data;
using BookQuotesBackend.Models;
using Microsoft.EntityFrameworkCore;


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

    internal async Task<bool> DeleteBook(int bookId, int userId)
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

    public async Task<Book> AddBook(BookRequest request, int userId)
    {
        Book book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublicationDate = request.PublishedDate,
            UserId = userId
        };

        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return book;
    }


    public async Task<bool> UpdateBook(int bookId, BookRequest request, int userId)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId && book.UserId == userId);
        if (book == null)
        {
            return false;
        }

        book.Title = request.Title;
        book.Author = request.Author;
        book.PublicationDate = request.PublishedDate;
        book.UserId = userId;

        await _context.SaveChangesAsync();

        return true;

    }
}