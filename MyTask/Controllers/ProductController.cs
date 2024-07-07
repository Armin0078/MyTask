using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MyTask.Data;
using MyTask.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly TaskDBContext _context;

		public ProductController(TaskDBContext context)
		{
			_context = context;
		}

		[HttpPost]
		[Authorize]
		public ActionResult CreateNewProduct([FromBody] Product product) 
		{
			try
			{
				product.CreatedByUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if(product.CreatedByUserId == 0 || product.CreatedByUserId == null)
				{
					return BadRequest("No User Authorized yet");
				}
				_context.Product.Add(product);
				_context.SaveChanges();
				return Ok("Congrats, We have new product");
			}
			catch (Exception)
			{
				throw;
			}
		}
		
	}
}
