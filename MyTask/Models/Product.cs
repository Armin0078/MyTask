using System.ComponentModel.DataAnnotations;

namespace MyTask.Models
{
	public class Product
	{
        public int Id { get; set; }

        [Required, MaxLength(25), MinLength(3)]
        public string? Name { get; set; }

		[Required]
		public DateTime ProduceDate { get; set; }

		[Required, MaxLength(11), Phone]
		public string? ManufacturePhone { get; set; }

		[Required, EmailAddress, MaxLength(30)]
		public string? ManufactureEmail { get; set; }

		
		public bool IsAvailable { get; set; }


		// relation with user model
		[Required]
		public int CreatedByUserId { get; set; }
		public User CreatedByUser { get; set; }
	}
}