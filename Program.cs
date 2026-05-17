using StudentStanding.Services;

var builder = WebApplication.CreateBuilder(args);

//register MVC and the singleton csv-backed data service
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<StudentDataService>();

var app = builder.Build();

//serve static files from wwwroot so bootstrap, the logo, and app.css load
app.UseStaticFiles();

//default route. a GET to / hits HomeController.Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
