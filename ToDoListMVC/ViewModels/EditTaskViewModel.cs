using ToDoListMVC.Models;

namespace ToDoListMVC.ViewModels
{
	public class EditTaskViewModel
	{
		public ToDoModel Task { get; set; }

		public IEnumerable<CategoriesModel> Categories { get; set; }
	}
}
