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
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<NewCourseDto, Course>();
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Enrollment, EnrollmentDto>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Index, opt => opt.Ignore());
        }
    }
}
