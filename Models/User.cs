namespace BookQuotesBackend.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public List<Book> Books { get; set; } = new();

    public List<Quote> Quotes { get; set; } = new();
}