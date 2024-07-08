using Microsoft.EntityFrameworkCore;
using MyTask.Data;
using MyTask.Models;

namespace MyTask.Repository
{
	public class ProductRepository
	{
		private readonly TaskDBContext _context;
		public ProductRepository(TaskDBContext context)
		{
			_context = context;
		}
		// User
		public User GetUserByName(string UserName)
		{
			try
			{
				return _context.User.SingleOrDefault(q => q.UserName == UserName);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public List<User> GetAllUser()
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
		//
		public List<Product> GetProductsByUserId(int userId)
		{
			try
			{
				return _context.Product.Where(u => u.CreatedByUserId == userId).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<Product> GetAllProducts()
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

		public Product GetProductByProductId(int productId)
		{
			try
			{
				return _context.Product.AsNoTracking().SingleOrDefault(u => u.Id == productId);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void SaveDBChanges()
		{
			try
			{
				_context.SaveChanges();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Add<T>(T product)
		{
			try
			{
				_context.Add(product);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void Delete<T>(T product)
		{
			try
			{
				_context.Remove(product);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public void Update<T>(T product)
		{
			try
			{
				_context.Update(product);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
