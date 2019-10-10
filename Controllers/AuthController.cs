using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using JWTAuth.Models;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace JWTAuth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<User> userManager;

        public AuthController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        // GET api/values
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var users = userManager.Users;
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {

                var token = generateJSONWebToken(model);

                return Ok(new
                {
                    token = token,
                });
            }
            return Unauthorized();
        }

        // POST api/auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new User { UserName = model.UserName, Email = model.Email };

            var result = await userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded) {
                return new BadRequestObjectResult(result.Errors.ToString());
            }

            return new OkObjectResult("Account created");
        }

        private string generateJSONWebToken(LoginModel userInfo)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Role, "Manager")
            };

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}