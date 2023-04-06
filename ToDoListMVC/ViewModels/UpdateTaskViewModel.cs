using ToDoListMVC.Models;

namespace ToDoListMVC.ViewModels
{
	public class UpdateTaskViewModel
	{
		public ToDoModel Task { get; set; }
		public IEnumerable<CategoriesModel> Categories { get; set; }
	}
}
