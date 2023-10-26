using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Repository;
using Domain;
using BussinesLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EspacioDBContext>(options =>
{
	options.UseSqlServer(Environment.GetEnvironmentVariable("EspacioDataConnection"));
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IRepository<Usuario>, UsuarioMemoryRepository>();
builder.Services.AddSingleton<UsuarioLogic>();
builder.Services.AddSingleton<IRepository<Espacio>, EspacioMemoryRepository>();
builder.Services.AddSingleton<EspacioLogic>();
builder.Services.AddSingleton<Persistencia>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<EspacioDBContext>();
	context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}



app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
