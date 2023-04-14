using System.Xml.Serialization;
using ToDoListMVC.Enums;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Models;

namespace ToDoListMVC.Repositories
{
	public class XMLCategoriesRepository : ICategoriesRepository
	{
		public string StorageType => StorageTypes.XML;
		private readonly string connectionString = "E:\\Programming\\Sana Course Projects\\Паша і Олег\\ToDoList\\ToDoListMVC\\ToDoStorage.xml";

		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToDoDataStorage));

		public IEnumerable<CategoriesModel> GetCategories()
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Categories;
			};
		}

		public CategoriesModel GetCategory(int id)
		{
			ToDoDataStorage? toDoDataStorage;
			using (FileStream fs = new FileStream(connectionString, FileMode.Truncate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Categories.Find(item => item.Id == id);
			}
		}
	}
}
