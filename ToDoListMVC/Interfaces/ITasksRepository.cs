using ToDoListMVC.Models;
namespace ToDoListMVC.Interfaces
{
    public interface ITasksRepository
    {
        IEnumerable<ToDoModel> GetTasks();
        ToDoModel GetTask(int? id);
        void CreateTask(ToDoModel task);
        void UpdateTask(int id, ToDoModel task);
        void DeleteTask(int id);
    }
}
