using System.Xml.Serialization;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Models;

namespace ToDoListMVC.Repositories
{
	public class XMLCategoriesRepository : ICategoriesRepository
	{
		public string StorageType => "XML";
		private ToDoDataStorage? toDoDataStorage;
		private readonly IConfiguration configuration;
		private readonly string toDoStoragePath;


		private XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToDoDataStorage));

		public XMLCategoriesRepository(IConfiguration configuration)
		{
			this.configuration = configuration;
			toDoStoragePath = configuration.GetConnectionString("ToDoStoragePath");
		}

		public IEnumerable<CategoriesModel> GetCategories()
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.OpenOrCreate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Categories;
			};
		}

		public CategoriesModel GetCategory(int id)
		{
			using (FileStream fs = new FileStream(toDoStoragePath, FileMode.Truncate))
			{
				toDoDataStorage = (ToDoDataStorage?)xmlSerializer.Deserialize(fs);
				return toDoDataStorage.Categories.FirstOrDefault(item => item.Id == id);
			}
		}
	}
}
