namespace ToDoListMVC.Repositories;

using ToDoListMVC.Interfaces;
using Dapper;
using ToDoListMVC.DBHelper;
using ToDoListMVC.Models;

public class CategoriesRepository : ICategoriesRepository
{
	public string StorageType => "SQL";
	private readonly DapperContext context;

	public CategoriesRepository(DapperContext context)
	{
		this.context = context;
	}

	public IEnumerable<CategoriesModel> GetCategories()
	{
		var query = "SELECT * FROM Categories";
		using (var connection = context.CreateConnection())
			return connection.Query<CategoriesModel>(query).ToList();
	}

	public CategoriesModel GetCategory(int id)
	{
		var query = "SELECT * FROM Categories WHERE Id = @Id";
		using (var connection = context.CreateConnection())
			return connection.QuerySingleOrDefault<CategoriesModel>(query);
	}
}

