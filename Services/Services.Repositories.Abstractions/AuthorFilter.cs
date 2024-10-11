#nullable enable
namespace Services.Repositories.Abstractions;

public class AuthorFilter
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }
}