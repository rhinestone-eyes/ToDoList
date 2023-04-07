using ToDoListMVC.Models;
using ToDoListMVC.ViewModels;
namespace ToDoListMVC.Interfaces
{
    public interface ITasksRepository
    {
        IEnumerable<ToDoModel> GetTasks();
        IEnumerable<ToDoModel> GetCompletedTasks();
        IEnumerable<ToDoModel> GetUncompletedTasks();
        ToDoModel GetTask(int? id);
        void CreateTask(ToDoModel task);
        void EditTask(int id, ToDoModel task);
        void TaskIsDone(int id, DateTime DoneDate);
        void DeleteTask(int id);
    }
}
