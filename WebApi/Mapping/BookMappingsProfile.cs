using System.Linq;
using AutoMapper;
using Services.Abstractions;
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
            CreateMap<BookDto, BookOutputModel>(); //из extension
            CreateMap<BookInputModel, BookDto>()
                .ForMember(dto => dto.Id, expression => expression.Ignore())
                .ForMember(dto => dto.MainAuthor,
                    expression => expression.MapFrom(bookModel => new AuthorDto { Id = bookModel.MainAuthorID }))
                .ForMember(dto => dto.CoAuthors,
                    expression => expression.MapFrom(bookModel =>
                        bookModel.CoAuthorsIDs.Select(id => new AuthorDto { Id = id })))
                .ForMemberTrim(dto => dto.Title, model => model.Title)
                .ForMemberTrim(dto => dto.Description, model => model.Description);

            CreateMap<BookFilterModel, BookFilterDto>()
                .ForMember(dto => dto.CoAuthors,
                    expression => expression.MapFrom(model => model.CoAuthor == null ? null : new[] { model.CoAuthor }))
                .ForMemberTrim(dto => dto.Title, model => model.Title)
                .ForMemberTrim(dto => dto.Description, model => model.Description);
        }
    }
}