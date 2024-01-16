using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core;
using Project.Core.Dtos;
using Project.Persistence;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Project.Controllers
{
    public class AppControllerBase : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IConfiguration config;
        protected readonly IMapper mapper;

        public AppControllerBase(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.config = config;
            this.mapper = mapper;
        }

        protected string GenerateJsonErrorResponse(string property, string message)
        {
            var response = new JsonObject();
            var errors = new JsonObject();
            var messages = new JsonArray();
            messages.Add(message);
            errors.Add(property, messages);
            response.Add("errors", errors);

            return response.ToString();
        }

        protected int GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity.Claims;

            return Convert.ToInt32(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value);
        }

        protected object PaginatedList<T>(PaginationDto pagination ,int totalCount, IEnumerable<T> data) where T : class
        {
            if (pagination == null)
                return data;

            return new
            {
                PageIndex = pagination.PageIndex,
                PageLength = pagination.PageLength,
                TotalCount = totalCount,
                Data = data
            };

        }
    }
}
