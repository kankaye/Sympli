namespace Sympli.Application.Domain;

public record SearchResult
{
    public required string Title { get; set; }

    public required string Url { get; set; }

    public required string? Snippet { get; set; }
}

