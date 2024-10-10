using AutoMapper;
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
                .ForMember(dto => dto.Id, expression => expression.Ignore());
        }
    }
}