﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.Domains;

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
        public IActionResult Withdraw(double amount)
        {
            var user = unitOfWork.Users.Get(GetUserId());
            if (user.Balance < amount)
                return BadRequest("Your account does not have enough balance to complete this withdraw");

            var withdrawType = unitOfWork.PaymentWithdrawTypes.SingleOrDefault(pwt => pwt.Name == PaymentWithdrawTypeNames.Withdraw);

            var withdraw = new PaymentWithdraw()
            {
                UserId = GetUserId(),
                Date = DateTime.Now,
                Type = withdrawType,
                Amount = amount
            };
            user.Balance -= amount;
            user.PaymentWithdraws.Add(withdraw);
            unitOfWork.Complete();

            return Ok();
        }
    }
}
