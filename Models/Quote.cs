namespace BookQuotesBackend.Models;

public class Quote
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public int UserId { get; set; }

    public User? User { get; set; }
}