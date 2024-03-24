var builder = WebApplication.CreateBuilder(args);

//Add-start
builder.Services.AddControllersWithViews();
//Add-end

var app = builder.Build();

//Update-start
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"
    );
//Update-end

app.Run();
