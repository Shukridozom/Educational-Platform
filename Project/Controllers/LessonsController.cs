using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlX.XDevAPI;
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

        [HttpGet("{courseId}")]
        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        public IActionResult Get(int courseId)
        {
            var courseWithLessons = unitOfWork.Course.GetCourseWithLessons(courseId);
            if (User.IsInRole(RoleName.Author) && courseWithLessons.UserId != GetUserId())
                return NotFound();

            if (User.IsInRole(RoleName.Student) && !unitOfWork.Enrollments.IsEnrolled(GetUserId(), courseWithLessons.Id))
                return NotFound();

            var lessonsDto = new List<LessonDto>();
            foreach (var lesson in courseWithLessons.Lessons)
                lessonsDto.Add(mapper.Map<Lesson, LessonDto>(lesson));

            return Ok(lessonsDto);
        }

        [HttpGet("{courseId}/{lessonId}")]
        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        public IActionResult Get(int courseId, byte lessonId)
        {
            var lesson = unitOfWork.Lessons.GetLessonWithCourse(courseId, lessonId);
            if (lesson == null)
                return NotFound();

            if (User.IsInRole(RoleName.Author) && lesson.Course.UserId != GetUserId())
                return NotFound();

            if (User.IsInRole(RoleName.Student) && !unitOfWork.Enrollments.IsEnrolled(GetUserId(), lesson.CourseId))
                return NotFound();

            return Ok(mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpGet("{courseId}/{index}")]
        [Authorize(Roles = $"{RoleName.Author},{RoleName.Student}")]
        public IActionResult GetByIndex(int courseId, int index)
        {
            var lesson = unitOfWork.Lessons.SingleOrDefault(l => l.CourseId == courseId && l.Index == index);
            var course = unitOfWork.Course.Get(courseId);
            if (lesson == null)
                return NotFound();

            if (User.IsInRole(RoleName.Author) && course.UserId != GetUserId())
                return NotFound();

            if (User.IsInRole(RoleName.Student) && !unitOfWork.Enrollments.IsEnrolled(GetUserId(), lesson.CourseId))
                return NotFound();

            return Ok(mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Post(LessonDto dto)
        {
            var course = unitOfWork.Course.Get(dto.CourseId);

            if (course == null)
                return BadRequest("unvalid courseId");

            if (course.UserId != GetUserId())
                return BadRequest("unvalid courseId");

            var lesson = mapper.Map<LessonDto, Lesson>(dto);
            lesson.Index = unitOfWork.Lessons.Count(l => l.CourseId == dto.CourseId) + 1;
            course.Lessons.Add(lesson);
            unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { lesson.CourseId, lesson.Id }, mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpPut]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Put(int courseId, byte lessonId, LessonDto dto)
        {
            var lesson = unitOfWork.Lessons.GetLessonWithCourse(courseId, lessonId);
            if (lesson == null)
                return NotFound();

            if (lesson.Course.UserId != GetUserId())
                return NotFound();

            mapper.Map(dto,lesson);
            unitOfWork.Complete();

            return Ok();
        }

        [HttpPatch]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult UpdateLessonIndex(int courseId, byte lessonId, int index)
        {
            var lesson = unitOfWork.Lessons.GetLessonWithCourse(courseId, lessonId);
            IEnumerable<Lesson> lessonsToUpdate;
            if (lesson == null)
                return NotFound();

            if (lesson.Course.UserId != GetUserId())
                return NotFound();

            if(lesson.Index > index)
            {
                lessonsToUpdate = unitOfWork.Lessons.Find(l => l.CourseId == courseId && l.Index >= index && l.Index < lesson.Index);
                foreach (var temp in lessonsToUpdate)
                    temp.Index++;
            }
            else if(lesson.Index < index)
            {
                lessonsToUpdate = unitOfWork.Lessons.Find(l => l.CourseId == courseId && l.Index <= index && l.Index > lesson.Index);
                foreach (var temp in lessonsToUpdate)
                    temp.Index--;
            }

            lesson.Index = index;

            unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Delete(int courseId, byte lessonId)
        {
            var lesson = unitOfWork.Lessons.GetLessonWithCourse(courseId, lessonId);
            if (lesson == null)
                return NotFound();

            if (lesson.Course.UserId != GetUserId())
                return NotFound();

            var lessonsToUpdate = unitOfWork.Lessons.Find(l => l.CourseId == courseId && l.Index > lesson.Index);
            foreach (var lessonToUpdate in lessonsToUpdate)
                lessonToUpdate.Index--;

            unitOfWork.Lessons.Remove(lesson);
            unitOfWork.Complete();

            return Ok();
        }

    }
}
