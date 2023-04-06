using Microsoft.AspNetCore.Mvc;

namespace ToDoListMVC.Controllers
{
	public class CategoriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
