using System.ComponentModel.DataAnnotations;

namespace MyTask.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(25)]
		public string? UserName { get; set; }
	}
}
