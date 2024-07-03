using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;
using System.Reflection.Emit;


namespace MyTask.Data
{
	public class TaskDBContext : DbContext
	{
		public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options)
		{
		}

		public DbSet<User> User { get; set; }
		public DbSet<Product> Product { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasKey(
				d => new { d.ManufactureEmail, d.ProduceDate }
				);


			base.OnModelCreating(modelBuilder);
		}
	}
}
