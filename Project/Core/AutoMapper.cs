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
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
