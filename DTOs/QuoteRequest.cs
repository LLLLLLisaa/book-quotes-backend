namespace BookQuotesBackend.DTOs;

public class QuoteRequest
{
    public string Text { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;
}