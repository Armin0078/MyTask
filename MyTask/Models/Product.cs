using System.ComponentModel.DataAnnotations;

namespace MyTask.Models
{
	public class Product
	{
		[Required, MaxLength(25), MinLength(3)]
        public string? Name { get; set; }

		[Required]
		public DateTime ProduceDate { get; set; }

		[Required, MaxLength(11), Phone]
		public string? ManufacturePhone { get; set; }

		[Required, EmailAddress, MaxLength(30)]
		public string? ManufactureEmail { get; set; }

		[Required]
		public bool IsAvailable { get; set; }
    }
}