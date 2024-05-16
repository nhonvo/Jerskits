using AutoMapper;
using ecommerce.Application.Common.Models;
using ecommerce.Application.Common.Models.User;

namespace ecommerce.Application.Common.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<User, LoginRequest>().ReverseMap();
        CreateMap<User, RegisterRequest>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, SignInResponse>().ReverseMap();

        CreateMap<User, UserProfileResponse>()
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.State, opt => opt.Ignore());
        CreateMap<User, UserUpdateRequest>().ReverseMap();

        CreateMap<UserProfileResponseCountry, Country>().ReverseMap();
        CreateMap<UserProfileResponseState, State>().ReverseMap();
        CreateMap<UserProfileResponseCity, City>().ReverseMap();

        CreateMap<LocationDTO, Country>().ReverseMap();
        CreateMap<LocationDTO, State>().ReverseMap();
        CreateMap<LocationDTO, City>().ReverseMap();
    }
}
