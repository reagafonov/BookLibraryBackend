using AutoMapper;
using Services.Abstractions;
using Services.Contracts;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi.Mapping;

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
            .ForMemberTrim(dto => dto.FirstName!, model => model.FirstName!)
            .ForMemberTrim(dto => dto.LastName!, model => model.LastName!);

        CreateMap<AuthorFilterModel, AuthorFilterDto>()
            .ForMemberTrim(x => x.FirstName!, x => x.FirstName!)
            .ForMemberTrim(x => x.LastName!, x => x.LastName!);
    }
}