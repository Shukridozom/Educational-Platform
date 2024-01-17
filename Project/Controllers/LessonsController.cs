using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.Domains;
using Project.Core.Dtos;

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
    }
}
