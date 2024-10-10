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
                .ForMember(book => book.Id, expression => expression.Ignore())
                .ForMember(book => book.Deleted, expression => expression.Ignore())
                .ForMember(book => book.MainAuthor, expression => expression.Ignore())
                .ForMember(book => book.CoAuthors, expression => expression.Ignore());
        }
    }
}