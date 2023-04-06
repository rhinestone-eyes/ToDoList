namespace ToDoListMVC;
using AutoMapper;
using ToDoListMVC.Models;
using ToDoListMVC.ViewModels;

public class MapperConfiguration : Profile
{
	public MapperConfiguration()
		{
			CreateMap<ToDoModel, CreateTaskViewModel>().ReverseMap();
		}

}
