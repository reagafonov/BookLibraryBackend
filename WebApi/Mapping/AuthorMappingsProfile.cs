using AutoMapper;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Models;

namespace WebApi.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности урока.
    /// </summary>
    public class AuthorMappingsProfile : Profile
    {
        public AuthorMappingsProfile()
        {
            CreateMap<AuthorDto, AuthorOutputModel>();
            CreateMap<AuthorInputModel, AuthorDto>()
                .ForMember(dto => dto.Id, expression => expression.Ignore())
                .ForMember(dto => dto.FirstName, expression => expression.MapFrom(model => model.FirstName.Trim()))
                .ForMember(dto => dto.LastName, expression => expression.MapFrom(model => model.LastName.Trim()));
            ;
            CreateMap<AuthorFilterModel, AuthorFilterDto>();
        }
    }
}