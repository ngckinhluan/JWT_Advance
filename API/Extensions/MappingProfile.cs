using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace API.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Request
        CreateMap<User, UserRequestDto>().ReverseMap();
        CreateMap<Role, RoleRequestDto>().ReverseMap();
        
        // Response
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<Role, RoleResponseDto>().ReverseMap();
        
        // RegisterRequestDto
        CreateMap<RegisterRequestDto, User>().ReverseMap();
        
       
    }
    
}