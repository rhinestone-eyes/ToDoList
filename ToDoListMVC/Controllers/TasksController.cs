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
            return View(new TasksIndexViewModel()
            {
                ToDos = tasks,
                Categories = categories,
            });
        }
        [HttpPost]
        public IActionResult Create(CreateTaskViewModel createTaskViewModel)
        {
            //ToDoModel task = mapper.Map<ToDoModel>(createTaskViewModel);
            var task = new ToDoModel()
            {
                CategoryId = createTaskViewModel.CategoryId,
                Name = createTaskViewModel.Name,
                DueDate = createTaskViewModel.DueDate,
            };
            if (!ModelState.IsValid) 
            {
                return View(task);
            }
            tasksRepository.CreateTask(task);
                return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update (int id)
        {
            var task = tasksRepository.GetTask(id);
            var categories = categoriesRepository.GetCategories();
            return View("UpdateTask", new UpdateTaskViewModel()
            {
                Task = task,
                Categories = categories
            });
        }
		[HttpPost]
		public IActionResult Update(ToDoModel task)
        {
			var id = task.Id;
            tasksRepository.UpdateTask(id, task);
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
