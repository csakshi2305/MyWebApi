using Microsoft.AspNetCore.Mvc;
using MyWebApi.Core.Model;
using MyWebApi.Infrastructure.Repository___service;
using MyWebApi.Interfaces;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController:ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            var response = await _emailService.GetUserDetailsByIDAsync(request.Id);
            return response.IsSuccess
                ? Ok(response.Message)
                : StatusCode(500, response.Message);
        }

    }
}

