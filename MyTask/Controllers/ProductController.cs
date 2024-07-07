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
		[Route("CreateNewProduct")]
		public ActionResult CreateNewProduct([FromBody] Product product) 
		{
			try
			{
				product.CreatedByUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if(product.CreatedByUserId == 0 || product.CreatedByUserId == null)
				{
					return BadRequest("No User has been Authorized yet");
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

		[HttpGet]
		[Authorize]
		[Route("GetProductByAuthorizedUser")]
		public ActionResult<List<Product>> GetProductByAuthorizedUser()
		{
			try
			{
				var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if (userId == 0 || userId == null)
				{
					return BadRequest("No Authorized User");
				}
				var products = _context.Product.Where(u => u.CreatedByUserId == userId).ToList();
				if (products.Count == 0) 
				{
					return BadRequest("This user has not any product");
				}
				return products;

			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpGet]
		[Route("GetAllProducts")]
		public ActionResult<List<Product>> GetAllProducts()
		{
			try
			{
				return _context.Product.ToList();
			}
			catch (Exception)
			{

				throw;
			}
		}


		[HttpDelete]
		[Authorize]
		[Route("DeleteProductByAuthorizedUser")]

		public ActionResult DeleteProductByAuthorizedUser(int productId)
		{
			try
			{
				var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if (userId == 0 || userId == null)
				{
					return BadRequest("No Authorized User");
				}
				var product = _context.Product.SingleOrDefault(u => u.Id == productId);
				if (product == null)
				{
					return BadRequest("This item does not exist");
				}
				if (product.CreatedByUserId != userId)
				{
					return BadRequest("This item is not for this user");
				}
				_context.Product.Remove(product);
				_context.SaveChanges();
				return Ok("Item Deleted");
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
