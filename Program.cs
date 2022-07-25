using LearnMathRu_0._1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Cors;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LavandaDB>();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddCors();





var app = builder.Build();




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

app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());



app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Orders}/{action=index}/{id?}");




//CreateDbIfNotExists(app);


//static void CreateDbIfNotExists(IHost host)
//{
//    using (var scope = host.Services.CreateScope())
//    {
//        var services = scope.ServiceProvider;
//        try
//        {
//            var context = services.GetRequiredService<LavandaDB>();
//            DBInitialize.Initialize(context);
//        }
//        catch (Exception ex)
//        {
//            var logger = services.GetRequiredService<ILogger<Program>>();
//            logger.LogError(ex, "An error occurred creating the DB.");
//        }
//    }
//}


app.Run();
