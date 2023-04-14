using ToDoListMVC;
using ToDoListMVC.DBHelper;
using ToDoListMVC.Interfaces;
using ToDoListMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ITasksRepository, TasksRepository>();
builder.Services.AddSingleton<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddSingleton<ITasksRepository, XMLTasksRepository>();
builder.Services.AddSingleton<ICategoriesRepository, XMLCategoriesRepository>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddAutoMapper(typeof(MapperConfigurationProfile));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
