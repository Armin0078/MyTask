using Microsoft.AspNetCore.Mvc;
using MyTask.Data;
using MyTask.Models;

namespace MyTask.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly TaskDBContext _context;

        public UserController(TaskDBContext context)
        {
            _context = context;
        }

		[HttpPost]
		public ActionResult InsertUser(User user)
		{
			try
			{
				_context.User.Add(user);
				_context.SaveChanges();
				return Ok("Completed");
			}
			catch (Exception)
			{
				throw;
			}
		}

		[HttpGet]
		public ActionResult<List<User>> GetAllUsers()
		{
			try
			{
				return _context.User.ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}
    }
}
