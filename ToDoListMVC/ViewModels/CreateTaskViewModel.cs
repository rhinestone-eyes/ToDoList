using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.ViewModels
{
	public class CreateTaskViewModel
	{
		[Required]
		[StringLength(maximumLength: 50, ErrorMessage = "Task description should be less than 50 symbols.")]
		[MinLength(2, ErrorMessage = "Task description should be more than 2 symbols.")]
		public string Name { get; set; }

		public int? CategoryId { get; set; }

		public DateTime? DueDate { get; set; }
	}
}
