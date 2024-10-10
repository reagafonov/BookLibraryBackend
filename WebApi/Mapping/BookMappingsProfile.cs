using System.Linq;
using AutoMapper;
using Services.Contracts;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности курса.
    /// </summary>
    public class BookMappingsProfile : Profile
    {
        public BookMappingsProfile()
        {
            CreateMap<BookDto, BookOutputModel>()
                .ForMember(dto => dto.MainAuthor,
                    expression => expression.MapFrom(bookDto => bookDto.MainAuthor.GetFullName())) //из extension
                .ForMember(dto => dto.CoAuthors,
                    expression =>
                        expression.MapFrom(bookDto =>
                            bookDto.CoAuthors.Select(coAuthor => coAuthor.GetFullName()))); //из extension
            CreateMap<BookInputModel, BookDto>()
                .ForMember(dto => dto.Id, expression => expression.Ignore())
                .ForMember(dto => dto.MainAuthor,
                    expression => expression.MapFrom(bookModel => new AuthorDto() { Id = bookModel.MainAuthorID }))
                .ForMember(dto => dto.CoAuthors,
                    expression => expression.MapFrom(bookModel =>
                        bookModel.CoAuthorsIDs.Select(id => new AuthorDto() { Id = id })));
        }
    }
}