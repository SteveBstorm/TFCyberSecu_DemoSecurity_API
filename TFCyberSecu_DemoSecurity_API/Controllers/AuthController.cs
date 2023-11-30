using DemoSecurity_BLL.Interface;
using DemoSecurity_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFCyberSecu_DemoSecurity_API.Models;
using TFCyberSecu_DemoSecurity_API.Tools;

namespace TFCyberSecu_DemoSecurity_API.Controllers
{
    //[Authorize("adminPolicy")]

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBLLService _userService;
        private readonly JwtGenerator _jwtGenerator;

        public AuthController(IUserBLLService userService, JwtGenerator jwtGenerator)
        {
            _userService = userService;
            _jwtGenerator = jwtGenerator;
        }
        //[AllowAnonymous] ==> A utiliser dans le cas ou le controller complet est limité (voir ligne 11)
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterFormDTO dto) 
        { 
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            _userService.Register(dto.Nickname, dto.Pwd, dto.Email);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginFormDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                User connectedUser = _userService.Login(dto.Email, dto.Password);
                string token = _jwtGenerator.Generate(connectedUser);
                return Ok(token);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize("adminPolicy")]
        [HttpGet("allUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

       
    }
}
