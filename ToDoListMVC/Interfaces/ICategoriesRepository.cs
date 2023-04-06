using ToDoListMVC.Models;


namespace ToDoListMVC.Interfaces
{
	public interface ICategoriesRepository
	{
		IEnumerable<CategoriesModel>GetCategories();
		CategoriesModel GetCategory(int? id);
	}
}
