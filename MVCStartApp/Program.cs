using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCStartApp.Middlewares;
using MVCStartApp.Models;
using MVCStartApp.Repository.Contracts;
using MVCStartApp.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value));
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddTransient<IRequestRepository, RequestRepository>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		   name: "default",
		   pattern: "{controller=Home}/{action=Index}/{id?}");

	//endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello world!"); });
	endpoints.MapGet("/config", async context =>
	{
		await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}");
	});
	endpoints.MapGet("/about", async context =>
	{
		await context.Response.WriteAsync($"{app.Environment.ApplicationName} - ASP.Net Core tutorial project");
	});

});

app.Run();
