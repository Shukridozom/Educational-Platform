using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project.Dtos;
using Project.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : AppControllerBase
    {
        public AccountsController(AppDbContext context, IConfiguration config, IMapper mapper)
            : base(context, config, mapper)
        {

        }

        [HttpGet]
        [Route("/api/accounts/{id}")]
        public IActionResult GetAccount(int id)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(mapper.Map<User, UserDto>(user));
        }

        [HttpPost("/api/register")]
        public IActionResult Register(RegisterDto userDto)
        {

            var userWithSameCredentials = context.Users
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
            context.Users.Add(user);

            context.SaveChanges();

            return CreatedAtAction(nameof(GetAccount), new { Id = user.Id }, mapper.Map<User, UserDto>(user));
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
            //Getting all roles except Admin:
            var rolesFromDb = context.Roles.Where(r => r.Name != RoleName.Admin).ToList();
            var rolesDto = new List<RoleDto>();
            foreach(var role in rolesFromDb)
            {
                rolesDto.Add(new RoleDto() { Id = role.Id, Name = role.Name});
            }

            return Ok(rolesDto);
        }

        private User AuthenticateUser(LoginDto loginCredentials)
        {
            var user = context.Users.Include(u => u.Role).SingleOrDefault(
                u => u.Username.ToLower() == loginCredentials.Username.ToLower());

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
