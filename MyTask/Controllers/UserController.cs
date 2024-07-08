using Microsoft.AspNetCore.Mvc;
using MyTask.Data;
using MyTask.Models;
using MyTask.Repository;

namespace MyTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		//private readonly TaskDBContext _context;
		static private ProductRepository? _productRepository;
		public UserController(ProductRepository productRepository)
        {
            //_context = context;
			_productRepository = productRepository;
        }

		[HttpPost]
		[Route("InsertUser")]
		public ActionResult InsertUser(User user)
		{
			try
			{
				//_context.User.Add(user);
				//_context.SaveChanges();
				_productRepository.Add<User>(user);
				_productRepository.SaveDBChanges();
				return Ok("Completed");
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet]
		[Route("GetAllUsers")]
		public ActionResult<List<User>> GetAllUsers()
		{
			try
			{
				return _productRepository.GetAllUser();
			}
			catch (Exception)
			{
				throw;
			}
		}
    }
}
