var builder = WebApplication.CreateBuilder(args);

//Add-start
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
//Add-end

var app = builder.Build();
app.UseStaticFiles();

app.UseSession();
//Update-start
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}"
    );
//Update-end

app.Run();
