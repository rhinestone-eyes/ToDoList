﻿using ToDoListMVC.Models;

namespace ToDoListMVC.ViewModels
{
	public class TasksIndexViewModel
	{
		public IEnumerable<ToDoModel> ToDos { get; set; }

		public IEnumerable<CategoriesModel> Categories { get; set; }

        public CreateTaskViewModel CreateTaskViewModel { get; set; }
	}
}
