using AutoMapper;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping
{
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
        }
    }
}