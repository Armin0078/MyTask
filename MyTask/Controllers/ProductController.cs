using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using MyTask.Data;
using MyTask.Models;
using MyTask.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		//private readonly TaskDBContext _context;
		static private ProductRepository? _productRepository;

		public ProductController(ProductRepository productRepository)
		{
			//_context = context;
			_productRepository = productRepository;
		}

		[HttpPost]
		[Authorize]
		[Route("CreateNewProduct")]
		public ActionResult CreateNewProduct([FromBody] Product product) 
		{
			try
			{
				product.CreatedByUserId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if(product.CreatedByUserId == 0)
				{
					return BadRequest("No User has been Authorized yet");
				}
				//_context.Product.Add(product);
				//_context.SaveChanges();
				_productRepository.Add<Product>(product);
				_productRepository.SaveDBChanges();
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
				if (userId == 0)
				{
					return BadRequest("No Authorized User");
				}
				//var products = _context.Product.Where(u => u.CreatedByUserId == userId).ToList();
				var products = _productRepository.GetProductsByUserId(userId);
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
				//return _context.Product.ToList();
				return _productRepository.GetAllProducts();
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpGet]
		[Route("FilterProductByMakerName")]
		public ActionResult<List<Product>> FilterProductByMakerName(string makerName)
		{
			try
			{
				//var maker = _context.User.SingleOrDefault(q => q.UserName == makerName);
				var maker = _productRepository.GetUserByName(makerName);
				if (maker == null)
				{
					return BadRequest("This user is not exist");
				}
				//var products = _context.Product.Where(u => u.CreatedByUserId == maker.Id).ToList();
				var products = _productRepository.GetProductsByUserId(maker.Id);
				if (products.Count == 0 || products == null)
				{
					return BadRequest("This user has not any products");
				}
				return products;
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpPut]
		[Route("UpdateProductByAuthorizedUser")]
		[Authorize]
		
		public ActionResult<Product> UpdateProductByAuthorizedUser(int productId,[FromBody] Product product)
		{
			try
			{
				var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
				if (userId == 0)
				{
					return BadRequest("No Authorized User");
				}
				//var oldProduct = _context.Product.AsNoTracking().SingleOrDefault(u => u.Id == productId);
				var oldProduct = _productRepository.GetProductByProductId(productId);	
				if (oldProduct == null)
				{
					return BadRequest("This item does not exist");
				}
				if (oldProduct.CreatedByUserId != userId)
				{
					return BadRequest("This item is not for this user");
				}
				product.CreatedByUserId = userId;
				product.Id = productId;
				product.ProduceDate = oldProduct.ProduceDate;
				//_context.Product.Update(product);
				//_context.SaveChanges();
				_productRepository.Update<Product>(product);
				_productRepository.SaveDBChanges();

				return Ok("item Updated");
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
				if (userId == 0)
				{
					return BadRequest("No Authorized User");
				}
				//var product = _context.Product.SingleOrDefault(u => u.Id == productId);
				var product = _productRepository.GetProductByProductId(productId);
				if (product == null)
				{
					return BadRequest("This item does not exist");
				}
				if (product.CreatedByUserId != userId)
				{
					return BadRequest("This item is not for this user");
				}
				//_context.Product.Remove(product);
				_productRepository.Delete<Product>(product);
				//_context.SaveChanges();
				_productRepository.SaveDBChanges();
				return Ok("Item Deleted");
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
