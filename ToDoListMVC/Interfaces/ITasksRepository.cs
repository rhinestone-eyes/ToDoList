using ToDoListMVC.Models;

namespace ToDoListMVC.Interfaces
{
    public interface ITasksRepository
    {
        IEnumerable<ToDoModel> GetTasks();

        ToDoModel GetTask(int id);

        void CreateTask(ToDoModel task);

        void EditTask(int id, ToDoModel task);

        void TaskIsDone(int id, DateTime DoneDate);

        void DeleteTask(int id);

		public string StorageType { get; }
	}
}
