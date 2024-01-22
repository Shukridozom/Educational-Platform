using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Project.Core;
using Project.Core.Domains;
using Project.Core.Dtos;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : AppControllerBase
    {
        public CoursesController(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
            :base(unitOfWork, configuration, mapper)
        {

        }

        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        [HttpGet]
        public IActionResult Get() 
        {

            if(User.IsInRole(RoleName.Author))
            {
                var courses = unitOfWork.Course.GetAuthorCoursesWithEnrollmentsCount(GetUserId());
                var coursesDto = new List<CourseForAuthorsDto>();
                foreach (var course in courses)
                    coursesDto.Add(new CourseForAuthorsDto()
                    {
                        NummberOfEnrollments = course.NummberOfEnrollments,
                        CourseDto = course.CourseDto
                    });

                return Ok(coursesDto);
            }
            else
            {
                var courses = unitOfWork.Course.GetAll();
                var coursesDto = new List<CourseDto>();
                foreach (var course in courses)
                {
                    coursesDto.Add(mapper.Map<Course, CourseDto>(course));
                }
                return Ok(coursesDto);
            }
        }

        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = unitOfWork.Course.Get(id);
            if (course == null)
                return NotFound();

            var courseDto = mapper.Map<Course, CourseDto>(course);

            if (course.UserId == GetUserId() && User.IsInRole(RoleName.Author))
            {
                var numberOfEnrollments = unitOfWork.Enrollments.Count(en => en.CourseId == course.Id);
                return Ok(new CourseForAuthorsDto()
                {
                    NummberOfEnrollments = numberOfEnrollments,
                    CourseDto = courseDto
                });
            }

            return Ok(courseDto);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Post(NewCourseDto dto)
        {
            var course = mapper.Map<NewCourseDto, Course>(dto);
            course.Date = DateTime.Now;
            course.UserId = GetUserId();
            unitOfWork.Course.Add(course);
            unitOfWork.Complete();

            return Created(Request.GetDisplayUrl() + $"/{course.Id}", mapper.Map<Course, CourseDto>(course));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Put(int id, NewCourseDto dto)
        {
            var course = unitOfWork.Course.SingleOrDefault(c => c.Id == id && c.UserId == GetUserId());
            if (course == null)
                return NotFound();

            mapper.Map(dto, course);
            unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Delete(int id)
        {
            var course = unitOfWork.Course.SingleOrDefault(c => c.Id == id && c.UserId == GetUserId());
            if (course == null)
                return NotFound();

            unitOfWork.Course.Remove(course);
            unitOfWork.Complete();

            return Ok();
        }


    }
}
