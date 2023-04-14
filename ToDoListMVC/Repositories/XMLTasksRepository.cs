using System.Xml.Serialization;
using ToDoListMVC.Enums;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Models;


namespace ToDoListMVC.Repositories
{
	public class XMLTasksRepository : ITasksRepository
	{
		public string StorageType => StorageTypes.XML;
		private readonly string connectionString = "E:\\Programming\\Sana Course Projects\\Паша і Олег\\ToDoList\\ToDoListMVC\\ToDoStorage.xml";
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToDoDataStorage));

		public IEnumerable<ToDoModel>? GetTasks()
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Tasks;
			};
		}

		public ToDoModel GetTask(int id)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Tasks.Find(item => item.Id == id);
			}
		}

		public void CreateTask(ToDoModel task)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				if (toDoDataStorage.Tasks == null)
					task.Id = 1;
				else
					task.Id = toDoDataStorage.Tasks.Count() + 1;
			}
			using (FileStream fs = new FileStream(connectionString, FileMode.Truncate))
			{
				toDoDataStorage.Tasks.Add(task);
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void DeleteTask(int id)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				var itemToRemove = toDoDataStorage.Tasks.SingleOrDefault(item => item.Id == id);
				toDoDataStorage?.Tasks.Remove(itemToRemove);
				var count = 0;
				foreach (var item in toDoDataStorage.Tasks)
				{
					count++;
					item.Id = count;
				}
			}
			using (FileStream fs = new FileStream(connectionString, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void EditTask(int id, ToDoModel task)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				var itemToEditIndex = toDoDataStorage.Tasks.FindIndex(item => item.Id == id);
				toDoDataStorage.Tasks[itemToEditIndex] = task;
			}
			using (FileStream fs = new FileStream(connectionString, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void TaskIsDone(int id, DateTime DoneDate)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				var itemToUpdate = toDoDataStorage.Tasks.SingleOrDefault(task => task.Id == id);
				itemToUpdate.DueDate = DoneDate;
				itemToUpdate.Status = true;
			}
			using (FileStream fs = new FileStream(connectionString, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}
	}
}
