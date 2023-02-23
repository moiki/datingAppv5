using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AppUser, MemberDto>()
            .ForMember(destination => destination.Age,
                option => option.MapFrom(src => src.BirthDate.GetAge()))
            .ForMember(destination => destination.PhotoUrl,
                options => options.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
        CreateMap<Photo, PhotoDto>();

        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.BirthDate,
                option => option.MapFrom(src => src.DateOfBirth));
        CreateMap<MemberUpdateDto, AppUser>();
    }
}