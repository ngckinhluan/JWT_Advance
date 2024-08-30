using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

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
        
       
    }
    
}