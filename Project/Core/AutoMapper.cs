using AutoMapper;
using Project.Core.Domains;
using Project.Core.Dtos;

namespace Project.Core
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<NewCourseDto, Course>();
            CreateMap<Course, CourseDto>();
        }
    }
}
