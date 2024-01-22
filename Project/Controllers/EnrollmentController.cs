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
    public class EnrollmentController : AppControllerBase
    {

        public EnrollmentController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {

        }

        [Authorize(Roles = RoleName.Student)]
        [HttpGet("/api/enrollments")]
        public IActionResult Get()
        {
            
            var enrollments = unitOfWork.Enrollments.Find(en => en.UserId == GetUserId());
            var enrollemntDtos = new List<EnrollmentDto>();

            foreach (var enrollment in enrollments)
                enrollemntDtos.Add(mapper.Map<Enrollment, EnrollmentDto>(enrollment));

            return Ok(enrollemntDtos);
        }

        [Authorize(Roles = RoleName.Student)]
        [HttpPost("enroll/{courseId}")]
        public IActionResult Post(int courseId)
        {
            var courseWithAuthor = unitOfWork.Course.GetCourseWithAuthor(courseId);
            var profitPercentage = unitOfWork.SystemVariables.GetProfitPercentageValue();
            var student = unitOfWork.Users.GetStudentWithEnrollments(GetUserId());

            if (courseWithAuthor == null)
                return BadRequest();

            if(student.Enrollments.SingleOrDefault(en => en.CourseId == courseId) != null)
                return BadRequest("Already enrolled in this course");

            if (courseWithAuthor.Price > student.Balance)
                return BadRequest("You don't have enough money in your account to complete this process");

            var enrollment = new Enrollment() 
            { 
                UserId = student.Id,
                CourseId = courseId, 
                Date = DateTime.Now, 
                Price = courseWithAuthor.Price, 
                AdminPortionOfPrice = profitPercentage * courseWithAuthor.Price
            };

            courseWithAuthor.User.Balance += enrollment.Price - enrollment.AdminPortionOfPrice;
            student.Balance -= enrollment.Price;
            student.Enrollments.Add(enrollment);

            unitOfWork.Complete();

            return Ok();
        }
    }
}
