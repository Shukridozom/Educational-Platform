using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.Domains;
using System.Security.Cryptography.X509Certificates;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RoleName.Admin)]
    public class SystemBalanceController : AppControllerBase
    {
        public SystemBalanceController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {


        }

        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            return Ok(unitOfWork.PaymentWithdraw.GetSystemBalance());
        }

        [HttpGet("profit")]
        public IActionResult GetProfit()
        {
            return Ok(unitOfWork.Enrollments.GetSystemProfit());
        }

    }
}
