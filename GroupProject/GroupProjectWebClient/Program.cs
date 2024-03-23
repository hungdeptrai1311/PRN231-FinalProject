var builder = WebApplication.CreateBuilder(args);

//Add-start
builder.Services.AddControllersWithViews();
//Add-end

var app = builder.Build();

//Update-start
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Index}/{id?}"
    );
//Update-end

app.Run();
