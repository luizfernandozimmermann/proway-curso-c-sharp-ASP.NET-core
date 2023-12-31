using LojaMvc.DependencyInjections;
using LojaMvc.Middlewares;
using LojaRepositorios.database;
using LojaRepositorios.DependencyInjections;
using LojaServicos.DependencyInjections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddServicesDependencyInjection()
    .AddRepositoriesDependencyInjection(builder.Configuration)
    .AddMvcAutoMapper()
    .AddLojaAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseSession()
    .UseMiddleware<AutenticacaoMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scopo = app.Services.CreateScope())
{
    var contexto = scopo.ServiceProvider.GetService<LojaContexto>();
    if (contexto != null)
    {
        contexto.Database.Migrate();
    }
}

app.Run();
