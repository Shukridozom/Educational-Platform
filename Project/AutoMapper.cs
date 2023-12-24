using AutoMapper;
using Project.Dtos;
using Project.Models;

namespace Project
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<User, UserDto>();

        }
    }
}
