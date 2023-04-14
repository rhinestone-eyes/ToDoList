using ToDoListMVC.Models;
using ToDoListMVC.DBHelper;
using Dapper;
using System.Data;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Enums;

namespace ToDoListMVC.Repositories
{
    public class TasksRepository : ITasksRepository
	{
		public string StorageType => StorageTypes.SQL;
		private readonly DapperContext context;

		public TasksRepository(DapperContext context)
		{
			this.context = context;
		}

		public IEnumerable<ToDoModel> GetTasks()
		{
			var query = "SELECT * FROM Tasks";

			using (var connection = context.CreateConnection())
				return connection.Query<ToDoModel>(query).ToList();
		}

		public ToDoModel GetTask(int id)
		{
			var query = "SELECT * FROM Tasks WHERE Id = @Id";

			using (var connection = context.CreateConnection())
				return connection.QuerySingleOrDefault<ToDoModel>(query, new { Id = id });
		}

		public void CreateTask(ToDoModel task)
		{
			var query = "INSERT INTO Tasks (CategoryID, Name, DueDate) VALUES (@CategoryID, @Name, @DueDate)";

			var parameters = new DynamicParameters();
			parameters.Add("CategoryID", task.CategoryId, DbType.Int32);
			parameters.Add("Name", task.Name, DbType.String);
			parameters.Add("DueDate", task.DueDate, DbType.Date);

			using (var connection = context.CreateConnection())
				connection.Execute(query, parameters);
		}

		public void EditTask(int id, ToDoModel task)
		{
			var query = "UPDATE Tasks SET Name = @Name, Status = @Status, DueDate = @DueDate, CategoryId = @CategoryId Where Id = @Id";
			var parameters = new DynamicParameters();
			parameters.Add("Id", task.Id, DbType.Int32);
			parameters.Add("CategoryID", task.CategoryId, DbType.Int32);
			parameters.Add("Name", task.Name, DbType.String);
			parameters.Add("DueDate", task.DueDate, DbType.Date);
			parameters.Add("Status", task.Status, DbType.Boolean);
			using (var connection = context.CreateConnection())
				connection.Execute(query, parameters);
		}

		public void TaskIsDone(int id, DateTime DoneDate)
		{
			var query = "UPDATE Tasks SET DueDate = @DoneDate, Status = 1 WHERE Id = @Id";

			using (var connection = context.CreateConnection())
				connection.Execute(query, new { DoneDate = DoneDate, Id = id });
		}

		public void DeleteTask(int id)
		{
			var query = "DELETE FROM Tasks WHERE Id = @Id";

			using (var connection = context.CreateConnection())
				connection.Execute(query, new {Id = id});
		}
	}
}
