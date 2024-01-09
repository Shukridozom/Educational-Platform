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
    public class TransfersController : AppControllerBase
    {
        public TransfersController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {
                
        }


        [Authorize(Roles = RoleName.Author)]
        [HttpPost("withdraw")]
        public IActionResult Withdraw(WithdrawDto dto)
        {
            var user = unitOfWork.Users.Get(GetUserId());
            if (user.Balance < dto.Amount)
                return BadRequest("Your account does not have enough balance to complete this withdraw");

            var withdrawType = unitOfWork.PaymentWithdrawTypes.SingleOrDefault(pwt => pwt.Name == PaymentWithdrawTypeNames.Withdraw);

            var withdraw = new PaymentWithdraw()
            {
                UserId = GetUserId(),
                Date = DateTime.Now,
                Type = withdrawType,
                Amount = dto.Amount
            };
            user.Balance -= dto.Amount;
            user.PaymentWithdraws.Add(withdraw);
            unitOfWork.Complete();

            return Ok();
        }

        [Authorize(Roles = RoleName.Admin)]
        [HttpPost("Deposit")]
        public IActionResult Deposit(DepositDto dto)
        {
            var student = unitOfWork.Users.Get(dto.UserId);
            var depositType = unitOfWork.PaymentWithdrawTypes.SingleOrDefault(pwt => pwt.Name == PaymentWithdrawTypeNames.Payment);

            if (student == null)
                return NotFound("user was not found");

            if (student.RoleId != unitOfWork.Roles.GetStudentRoleId())
                return BadRequest("This user is not authorized as a student");

            var deposit = new PaymentWithdraw()
            {
                UserId = student.Id,
                Date = DateTime.Now,
                Type = depositType,
                Amount = dto.Amount
            };

            student.Balance += dto.Amount;
            student.PaymentWithdraws.Add(deposit);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
