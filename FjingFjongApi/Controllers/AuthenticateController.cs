using Microsoft.AspNetCore.Mvc;
using FjingFjongApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using FjingFjongApi.Services;
using System.Threading.Tasks;

namespace FjingFjongApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        public class AuthObject
        {
            public string API_KEY { get; set; }
        }

        // GET: /Authenticate
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] AuthObject AuthObject)
        {
            string token = await _userService.Authenticate(AuthObject.API_KEY);

            if (token == null)
                return BadRequest(new { message = "Invalid API Key." });

            return Ok(new { Token = token });
        }
    }
}