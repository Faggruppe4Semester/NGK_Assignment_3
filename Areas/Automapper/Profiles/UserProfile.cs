using AutoMapper;
using NGK_Assignment_3.Areas.Database.Models;

namespace NGK_Assignment_3.Areas.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>();
        }
    }
}