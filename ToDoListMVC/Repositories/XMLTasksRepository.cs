using System.Xml.Serialization;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Models;


namespace ToDoListMVC.Repositories
{
	public class XMLTasksRepository : ITasksRepository
	{
		public string StorageType => "XML";
		private ToDoDataStorage? toDoDataStorage;
		private readonly IConfiguration configuration;
		private readonly string toDoStoragePath;


		private XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToDoDataStorage));

		public XMLTasksRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
			toDoStoragePath = configuration.GetConnectionString("ToDoStoragePath");
		}

		public IEnumerable<ToDoModel>? GetTasks()
		{
			
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Tasks;
			};
		}

		public ToDoModel GetTask(int id)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Tasks.Find(item => item.Id == id);
			}
		}

		public void CreateTask(ToDoModel task)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				if (toDoDataStorage.Tasks == null)
					task.Id = 1;
				else
					task.Id = toDoDataStorage.Tasks.Count() + 1;
			}
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.Truncate))
			{
				toDoDataStorage.Tasks.Add(task);
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void DeleteTask(int id)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
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
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void EditTask(int id, ToDoModel task)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				var itemToEditIndex = toDoDataStorage.Tasks.FindIndex(item => item.Id == id);
				toDoDataStorage.Tasks[itemToEditIndex] = task;
			}
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}

		public void TaskIsDone(int id, DateTime DoneDate)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				var itemToUpdate = toDoDataStorage.Tasks.SingleOrDefault(task => task.Id == id);
				itemToUpdate.DueDate = DoneDate;
				itemToUpdate.Status = true;
			}
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.Truncate))
			{
				xmlSerializer.Serialize(fs, toDoDataStorage);
			}
		}
	}
}
