using Afforestation.App.Users;
using Afforestation.Domain.Dtos;
using Afforestation.Domain.Infrastructure;
using Afforestation.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AfforestationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        private readonly IConfiguration _Config;

        public UserController(IUserManager userManager, IConfiguration config)
        {
            _userManager = userManager;
            _Config = config;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister userRegisterDto)
        {   //validation

            userRegisterDto.UserName.ToLower();
            if (await _userManager.UserExist(userRegisterDto.UserName, userRegisterDto.Phone))//see if user is alredy exisit in db
            {
                return BadRequest("that user alrady exiset");
            }

            if(userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            {
                return BadRequest("uncorrect password");
            }
            var newUser = new Users
            {
                UserName = userRegisterDto.UserName,
                FullName = userRegisterDto.FullName,
                Phone = userRegisterDto.Phone
            };
            await _userManager.Register(newUser, userRegisterDto.Password);
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLoginDto)
        {
            var user = await _userManager.Login(userLoginDto.UserName, userLoginDto.Phone, userLoginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }




        [HttpGet("{id}")]
        public IActionResult GetUserById(int id, [FromServices] GerUser gerUser) =>
        Ok(gerUser.Do(id));

    }
}
