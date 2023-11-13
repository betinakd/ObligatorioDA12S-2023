using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Repository;
using Domain;
using BussinesLogic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsuariosDbContext>
	(options => options.UseSqlServer
	(builder.Configuration.GetConnectionString("UsuariosDbConection"),
		providerOptions => providerOptions.EnableRetryOnFailure()));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioMemoryRepository>();
builder.Services.AddScoped<UsuarioLogic>();
builder.Services.AddScoped<IRepository<Espacio>, EspacioMemoryRepository>();
builder.Services.AddScoped<EspacioLogic>();
builder.Services.AddSingleton<Persistencia>();
builder.Services.AddMudServices();


var app = builder.Build();

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