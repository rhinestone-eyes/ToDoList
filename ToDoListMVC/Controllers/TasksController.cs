using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListMVC.Models;
using ToDoListMVC.ViewModels;
using ToDoListMVC.Repositories;
using ToDoListMVC.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;


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
			var tasks = tasksRepository.GetTasks();
			var categories = categoriesRepository.GetCategories();
			var uncompletedTasks = tasksRepository.GetUncompletedTasks();
			var completedTasks = tasksRepository.GetCompletedTasks();
			return View(new TasksIndexViewModel()
			{
				ToDos = tasks,
				Categories = categories,
				UncompletedTasks = uncompletedTasks,
				CompletedTasks = completedTasks
			});
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
				var toDos = tasksRepository.GetTasks();
				var categories = categoriesRepository.GetCategories();
				var completedTasks = tasksRepository.GetCompletedTasks();
				var uncompletedTasks = tasksRepository.GetUncompletedTasks();

				return View("Index", new TasksIndexViewModel()
				{
					ToDos = toDos,
					Categories = categories,
					UncompletedTasks = uncompletedTasks,
					CompletedTasks = completedTasks,
				});
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

	}
}
