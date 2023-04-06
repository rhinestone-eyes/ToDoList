using System.ComponentModel.DataAnnotations;

namespace ToDoListMVC.Models
{
	public class ToDoModel
	{
		public int Id { get; set; }
		[Required]
		[StringLength(maximumLength:60, ErrorMessage = "Task description should be between 2 and 60 symbols.", MinimumLength =2)]
		public string Name { get; set; }
		public int CategoryId {get; set; }
		public DateTime DueDate { get; set; }
		public bool Status { get; set; }
	}
}
