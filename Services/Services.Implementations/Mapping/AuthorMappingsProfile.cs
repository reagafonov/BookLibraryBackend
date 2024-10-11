using AutoMapper;
using Domain.Entities;
using Services.Abstractions;
using Services.Contracts;
using Services.Repositories.Abstractions;

namespace Services.Implementations.Mapping;

/// <summary>
/// Профиль автомаппера для сущности автора.
/// </summary>
public class AuthorMappingsProfile : Profile
{
    public AuthorMappingsProfile()
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<AuthorDto, Author>()
            .ForMember(author => author.Deleted, expression => expression.Ignore())
            .ForMember(author => author.BooksAuthor, expression => expression.Ignore())
            .ForMember(author => author.BookCoAuthors, expression => expression.Ignore());

        CreateMap<AuthorFilterDto, AuthorFilter>();
    }
}