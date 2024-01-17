using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Project.Core;
using Project.Core.Domains;
using Project.Core.Dtos;
using System.Security.Principal;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : AppControllerBase
    {
        public LessonsController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {

        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        public IActionResult Get(int id)
        {
            var lesson = unitOfWork.Lessons.GetLessonWithCourse(id);
            if (lesson == null)
                return NotFound();

            if (User.IsInRole(RoleName.Author) && lesson.Course.UserId != GetUserId())
                return NotFound();

            if (User.IsInRole(RoleName.Student) && !unitOfWork.Enrollments.IsEnrolled(GetUserId(), id))
                return NotFound();

            return Ok(mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Post(LessonDto dto)
        {
            var course = unitOfWork.Course.GetCourseWithLessons(dto.CourseId);

            if (course == null)
                return BadRequest("unvalid courseId");

            if (course.UserId != GetUserId())
                return BadRequest("unvalid courseId");

            var lesson = mapper.Map<LessonDto, Lesson>(dto);
            lesson.Index =(byte) (course.Lessons.Count + 1);
            course.Lessons.Add(lesson);
            unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { Id = lesson.Id }, mapper.Map<Lesson, LessonDto>(lesson));
        }
    }
}
