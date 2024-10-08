using AutoMapper;
using Domain.Entities;
using Services.Contracts;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности книги.
    /// </summary>
    public class BookMappingsProfile : Profile
    {
        public BookMappingsProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<BookDto, Book>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore());
        }
    }
}