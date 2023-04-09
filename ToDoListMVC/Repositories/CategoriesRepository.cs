namespace ToDoListMVC.Repositories;
using ToDoListMVC.Interfaces;
using Dapper;
using System.Data;
using ToDoListMVC.DBHelper;
using ToDoListMVC.Models;

public class CategoriesRepository : ICategoriesRepository
{
	private readonly DapperContext context;

	public CategoriesRepository(DapperContext context)
	{
		this.context = context;
	}

	public IEnumerable<CategoriesModel> GetCategories()
	{
		var query = "SELECT * FROM Categories";
		using (var connection = context.CreateConnection())
		{
			var categories = connection.Query<CategoriesModel>(query);

			return categories.ToList();
		}
	}

	public CategoriesModel GetCategory(int? id)
	{
		var query = "SELECT * FROM Categories WHERE Id = @Id";
		using (var connection = context.CreateConnection())
		{
			var category = connection.QuerySingleOrDefault<CategoriesModel>(query);

			return category;
		}
	}
}

