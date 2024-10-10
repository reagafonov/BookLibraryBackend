using Services.Contracts;

namespace WebApi.Extensions;

public static class AuthorExtension
{
    public static string GetFullName(this AuthorDto dto) => $"{dto.FirstName} {dto.LastName}";
}