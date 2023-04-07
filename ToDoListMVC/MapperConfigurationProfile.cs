namespace ToDoListMVC;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using ToDoListMVC.Models;
using ToDoListMVC.ViewModels;


public class MapperConfigurationProfile : Profile
{
	public MapperConfigurationProfile()
	{
		CreateMap<ToDoModel, CreateTaskViewModel>().ReverseMap();
	}
}
