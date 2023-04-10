using Microsoft.AspNetCore.Mvc;
using ToDoListMVC.Models;
using ToDoListMVC.ViewModels;
using ToDoListMVC.Interfaces;
using AutoMapper;



namespace ToDoListMVC.Controllers
{
	public class TasksController : Controller
	{
		private readonly ITasksRepository tasksRepository;
		private readonly ICategoriesRepository categoriesRepository;
		private readonly IMapper mapper;

		public TasksController(ITasksRepository tasksRepository, ICategoriesRepository categoriesRepository, IMapper mapper)
		{
			this.tasksRepository = tasksRepository;
			this.categoriesRepository = categoriesRepository;
			this.mapper = mapper;
		}

		public IActionResult Index()
		{
			return View(GetTasksIndexViewModel());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CreateTaskViewModel createTaskViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", GetTasksIndexViewModel());
			}
			ToDoModel task = mapper.Map<ToDoModel>(createTaskViewModel);
			tasksRepository.CreateTask(task);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var task = tasksRepository.GetTask(id);
			var categories = categoriesRepository.GetCategories();

			return View("EditTask", new EditTaskViewModel()
			{
				Task = task,
				Categories = categories
			});
		}

		[HttpPost]
		public IActionResult Edit(ToDoModel task)
		{
			var id = task.Id;
			tasksRepository.EditTask(id, task);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult TaskIsDone(int id)
		{
			tasksRepository.TaskIsDone(id, DateTime.Now);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			tasksRepository.DeleteTask(id);

			return RedirectToAction("Index");
		}

		public TasksIndexViewModel GetTasksIndexViewModel()
		{
			var tasks = tasksRepository.GetTasks();
			var categories = categoriesRepository.GetCategories();

			return new TasksIndexViewModel()
			{
				ToDos = tasks,
				Categories = categories,
			};
		}
	}
}
