using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DssDbDemoApp.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DssDbDemoAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DssDbDemoAppContext") ?? throw new InvalidOperationException("Connection string 'DssDbDemoAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//============================code for auto updating database after db migration=================

using(var scope = app.Services.CreateScope())
{
    var services= scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DssDbDemoAppContext>();
        context.Database.Migrate();
    }catch(Exception ex) {
        var logger= services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An Error Occured Creating the db");
    }
}
//=============================================================END OF CODE========================

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
