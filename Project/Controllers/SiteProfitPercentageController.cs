using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.Domains;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteProfitPercentageController : AppControllerBase
    {
        public SiteProfitPercentageController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {

        }

        [Authorize(Roles =$"{RoleName.Admin},{RoleName.Author}")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(unitOfWork.SystemVariables.GetProfitPercentageValue());
        }

        [Authorize(Roles = RoleName.Admin)]
        [HttpPut]
        public IActionResult Put(double percentage)
        {
            if (percentage < 0.0 || percentage > 1.0)
                return BadRequest("Profit percentage must be between 0 and 1");

            SystemVariables profitPercentage = unitOfWork.SystemVariables
                .SingleOrDefault(sv => sv.Name == SystemVariablesName.ProfitPercentage);
            profitPercentage.Value = Math.Round(percentage, 2).ToString();

            unitOfWork.Complete();

            return Ok();
        }
    }
}
