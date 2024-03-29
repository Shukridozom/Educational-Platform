﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using Project.Core;
using Project.Core.Domains;
using Project.Core.Dtos;
using Project.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : AppControllerBase
    {
        public AccountsController(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
            : base(unitOfWork, config, mapper)
        {

        }

        [HttpGet]
        [Authorize]
        [Route("/api/account/{id?}")]
        public IActionResult Get(int id = 5465494) //Just a random initial value (because SwaggerUI doesn't seem to consider optional parameters
        {
            if (id == 5465494)
                id = GetUserId();

            var user = unitOfWork.Users.GetUserWithRole(id);
            if (user == null)
                return NotFound();

            var userDto = mapper.Map<User, UserDto>(user);

            if (id != GetUserId())
                return Ok(new
                {
                    userDto.Id,
                    userDto.Username,
                    userDto.Email,
                    userDto.FirstName,
                    userDto.LastName,
                    userDto.Role
                });

            return Ok(userDto);
        }

        [HttpPost("/api/register")]
        public IActionResult Register(RegisterDto userDto)
        {

            var roles = unitOfWork.Roles.GetRolesExceptAdmin();
            if (roles.SingleOrDefault(r => r.Id == userDto.RoleId) == null)
                return BadRequest("Unavailable roleId");

            var userWithSameCredentials = unitOfWork.Users
                .SingleOrDefault(u => u.Username.ToLower() == userDto.Username.ToLower()
                || u.Email.ToLower() == userDto.Email.ToLower());
            if (userWithSameCredentials != null)
            {
                if (userWithSameCredentials.Username == userDto.Username)
                    return Conflict(GenerateJsonErrorResponse("username", "Username already exists"));
                else
                    return Conflict(GenerateJsonErrorResponse("email", "Email already exists"));
            }

            var user = mapper.Map<RegisterDto, User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            unitOfWork.Users.Add(user);

            unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new { user.Id }, mapper.Map<User, UserDto>(user));
        }

        [HttpPost]
        [Route("/api/login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = AuthenticateUser(loginDto);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return BadRequest(GenerateJsonErrorResponse("username", "The username or password is incorrect"));
        }

        [HttpGet]
        [Route("/api/roles")]
        public IActionResult GetRoles()
        {
            var rolesFromDb = unitOfWork.Roles.GetRolesExceptAdmin();
            var rolesDto = new List<RoleDto>();
            foreach(var role in rolesFromDb)
            {
                rolesDto.Add(new RoleDto() { Id = role.Id, Name = role.Name});
            }

            return Ok(rolesDto);
        }

        private User AuthenticateUser(LoginDto loginCredentials)
        {
            var user = unitOfWork.Users.GetUserWithRole(loginCredentials.Username);

            if (user == null)
                return null;

            if (BCrypt.Net.BCrypt.Verify(loginCredentials.Password, user.PasswordHash))
                return user;

            return null;
        }


        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(Convert.ToDouble(config["Jwt:ValidForInMin"])),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
