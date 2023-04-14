using System.Xml.Serialization;
using ToDoListMVC.Models;
namespace ToDoListMVC;

[Serializable, XmlRoot("ToDoDataStorage")]
public class ToDoDataStorage
{
	public List<ToDoModel>? Tasks { get; set; }
	
	public List<CategoriesModel>? Categories { get; set; }
}
