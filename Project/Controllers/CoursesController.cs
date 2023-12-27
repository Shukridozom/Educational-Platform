using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = $"{RoleName.Admin},{RoleName.Author},{RoleName.Student}")]
        [HttpGet]
        public IActionResult Get() 
        {
            var courses = unitOfWork.Course.GetAll();
            return Ok(courses);
        }

        [Authorize(Roles = $"{RoleName.Admin},{RoleName.Author},{RoleName.Student}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = unitOfWork.Course.Get(id);
            if (course == null)
                return NotFound();

            return Ok(mapper.Map<Course, CourseDto>(course));
        }

    }
}
