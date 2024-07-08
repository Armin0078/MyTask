using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyTask.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(25), MinLength(2)]
		public string? Name { get; set; }

		[Required]
		public DateTime ProduceDate { get; set; }

		[Required, MaxLength(12), Phone]
		public string? ManufacturePhone { get; set; }

		[Required, EmailAddress, MaxLength(30)]
		public string? ManufactureEmail { get; set; }


		public bool IsAvailable { get; set; }


		// relation with user model
		[AllowNull]
		public int CreatedByUserId { get; set; }
	}
}