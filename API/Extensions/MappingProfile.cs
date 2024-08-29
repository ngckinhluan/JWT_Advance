using AutoMapper;
using BusinessObjects.DTO.Request;
using BusinessObjects.DTO.Response;
using BusinessObjects.Entities;

namespace API.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // RequestDto Mapping
        CreateMap<UserDetail, UserDetailRequestDto>().ReverseMap();
        
        // ResponseDto Mapping
        CreateMap<UserDetail, UserDetailResponseDto>().ReverseMap();
    }
    
}