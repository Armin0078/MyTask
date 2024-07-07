using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTask.Data;
using MyTask.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private IConfiguration _config;
		private readonly TaskDBContext _context;

		public LoginController(IConfiguration config, TaskDBContext context)
		{
			_config = config;
			_context = context;
		}

		[AllowAnonymous]
		[HttpPost]
		public IActionResult Login([FromBody] User login)
		{
			IActionResult response = Unauthorized();
			var user = AuthenticateUser(login);

			if (user != null)
			{
				var tokenString = GenerateJSONWebToken(user);
				response = Ok(new { token = tokenString });
			}

			return response;
		}

		private string GenerateJSONWebToken(User userInfo)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
				new Claim(ClaimTypes.Name, userInfo.UserName)
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
			  _config["Jwt:Issuer"],
			  _config["Jwt:Issuer"],
			  claims: claims,
			  expires: DateTime.Now.AddMinutes(120),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private User AuthenticateUser(User login)
		{
			var user = _context.User.SingleOrDefault(u => u.UserName == login.UserName);

			if (user == null)
			{
				return null;
			}

			if (user.Password == login.Password)
			{
				return user;
			}
			return null;
		}
	}
}
