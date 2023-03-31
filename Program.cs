using Microsoft.EntityFrameworkCore;
using Westcoast.web.Data;
using Westcoast.web.Interface;
using Westcoast.web.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WestcoastContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")) 
);

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try{
    var context = services.GetRequiredService<WestcoastContext>();
    await context.Database.MigrateAsync();
    await SeedData.LoadCoursesData(context);
}
catch(Exception ex){
    Console.WriteLine("{0} - {1}", ex.Message, "Något gick fel");
    
}


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
