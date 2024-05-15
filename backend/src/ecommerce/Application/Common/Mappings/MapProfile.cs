using AutoMapper;
using ecommerce.Application.Common.Models.User;

namespace ecommerce.Application.Common.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<User, LoginRequest>().ReverseMap();
        CreateMap<User, RegisterRequest>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();

        CreateMap<User, UserProfile>().ReverseMap();
    }
}
