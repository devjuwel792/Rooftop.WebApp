using Microsoft.EntityFrameworkCore;
using Rooftop.WebApp;
using Rooftop.WebApp.DatabaseContext;
using Rooftop.WebApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(x=>x.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddAutoMapper(typeof(ICore).Assembly);
builder.Services.Scan(x => x.FromAssemblyOf<ICore>()
.AddClasses(x => x.AssignableTo<ICore>())
.AddClasses(x => x.AssignableTo(typeof(IRepositoryService<,>)))
.AsSelfWithInterfaces()
.WithScopedLifetime());
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
