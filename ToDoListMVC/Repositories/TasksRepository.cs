using ToDoListMVC.Models;
using ToDoListMVC.DBHelper;
using Dapper;
using System.Data;
using ToDoListMVC.Interfaces;

namespace ToDoListMVC.Repositories
{
    public class TasksRepository : ITasksRepository
	{
		private readonly DapperContext context;

		public TasksRepository(DapperContext context)
		{
			this.context = context;
		}

		public IEnumerable<ToDoModel> GetTasks()
		{
			var query = "SELECT * FROM Tasks";

			using (var connection = context.CreateConnection())
			{
				var tasks = connection.Query<ToDoModel>(query);

				return tasks.ToList();
			}
		}

		public ToDoModel GetTask(int? id)
		{
			var query = "SELECT * FROM Tasks WHERE Id = @Id";
			using (var connection = context.CreateConnection())
			{
				var task = connection.QuerySingleOrDefault<ToDoModel>(query);

				return task;
			}
		}

		public void CreateTask(ToDoModel task)
		{
			var query = "INSERT INTO Tasks (CategoryID, Name, DueDate) VALUES (@CategoryID, @Name, @DueDate)";

			var parameters = new DynamicParameters();
			parameters.Add("CategoryID", task.CategoryId, DbType.Int32);
			parameters.Add("Name", task.Name, DbType.String);
			parameters.Add("DueDate", task.DueDate, DbType.Date);

			using (var connection = context.CreateConnection())
			{
				connection.Execute(query, parameters);
			}
		}
		public void UpdateTask(int id, ToDoModel task)
		{
			var query = "INSERT INTO Tasks (Id, CategoryID, Name, DueDate, Status) VALUES (@Id, @CategoryID, @Name, @DueDate, @Status WHERE Id = @Id)";
			var parameters = new DynamicParameters();
			parameters.Add("Id", task.Id, DbType.Int32);
			parameters.Add("CategoryID", task.CategoryId, DbType.Int32);
			parameters.Add("Name", task.Name, DbType.String);
			parameters.Add("DueDate", task.DueDate, DbType.Date);
			parameters.Add("Status", task.Status, DbType.Boolean);

			using (var connection = context.CreateConnection())
			{
				connection.Execute(query, parameters);
			}
		}

		public void DeleteTask(int id)
		{
			var query = "DELETE FROM Companies WHERE Id = @Id";
			using (var connection = context.CreateConnection())
			{
				connection.Execute(query, new {id});
			}
		}
	}
}
