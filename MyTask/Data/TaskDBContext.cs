using Microsoft.EntityFrameworkCore;
using MyTask.Models;


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
			modelBuilder.Entity<Product>()
			.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
			.IsUnique();


			base.OnModelCreating(modelBuilder);
		}
	}
}
