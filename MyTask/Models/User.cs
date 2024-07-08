using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTask.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please Enter your User Name"), MaxLength(25)]
		[MinLength(4)]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Please Enter your password")]
		[MinLength(8)]
		[MaxLength(16)]
		[PasswordPropertyText]
		public string? Password { get; set; }
	}
}
