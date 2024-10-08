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
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(x => x.BooksAuthor, x => x.Ignore())
                .ForMember(x => x.BooksCoAuthor, x => x.Ignore());
        }
    }
}