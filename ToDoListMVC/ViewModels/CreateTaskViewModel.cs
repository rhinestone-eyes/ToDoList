using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.ViewModels
{
	public class CreateTaskViewModel
	{
		[Required]
		[StringLength(maximumLength: 60, ErrorMessage = "Task description should be between 2 and 60 symbols.", MinimumLength = 2)]
		public string? Name { get; set; }
		public int CategoryId { get; set; }
		public DateTime DueDate { get; set; }
	}
}
