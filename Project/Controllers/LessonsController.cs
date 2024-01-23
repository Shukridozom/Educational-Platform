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
        public IActionResult GetAll(int courseId, [FromQuery]PaginationDto pagination)
        {
            var course = unitOfWork.Course.Get(courseId);
            var lessons = unitOfWork.Lessons.GetLessonsOrderedByIndex(courseId, pagination.PageIndex, pagination.PageLength);
            var numberOfLessons = unitOfWork.Lessons.Count(l => l.CourseId == courseId);
            if (User.IsInRole(RoleName.Author) && course.UserId != GetUserId())
                return NotFound();

            if (User.IsInRole(RoleName.Student) && !unitOfWork.Enrollments.IsEnrolled(GetUserId(), course.Id))
                return BadRequest("You're not enrolled in this course");

            var lessonsDto = new List<LessonDto>();
            foreach (var lesson in lessons)
                lessonsDto.Add(mapper.Map<Lesson, LessonDto>(lesson));

            return Ok(PaginatedList(pagination, numberOfLessons, lessonsDto));
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
                return BadRequest("You're not enrolled in this course");

            return Ok(mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpGet("getByIndex/{courseId}/{index}")]
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
                return BadRequest("You're not enrolled in this course");

            return Ok(mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpPost("{courseId}")]
        [Authorize(Roles = RoleName.Author)]
        public IActionResult Post(int courseId, LessonDto dto)
        {
            var course = unitOfWork.Course.Get(courseId);

            if (course == null)
                return BadRequest("unvalid courseId");

            if (course.UserId != GetUserId())
                return BadRequest("unvalid courseId");

            var lesson = mapper.Map<LessonDto, Lesson>(dto);
            lesson.Index = unitOfWork.Lessons.Count(l => l.CourseId == courseId) + 1;
            lesson.CourseId = courseId;
            course.Lessons.Add(lesson);
            unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new {  courseId = lesson.CourseId, lessonId = lesson.Id }, mapper.Map<Lesson, LessonDto>(lesson));
        }

        [HttpPut("{courseId}/{lessonId}")]
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

        [HttpPatch("{courseId}/{lessonId}/{index}")]
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

        [HttpDelete("{courseId}/{lessonId}")]
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
