using Repository;
using Domain;
using BussinesLogic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FintracDbContext>
	(options => options.UseSqlServer
	(builder.Configuration.GetConnectionString("FintracDbConection")));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IRepository<Usuario>, UsuarioMemoryRepository>();
builder.Services.AddSingleton<UsuarioLogic>();
builder.Services.AddSingleton<IRepository<Espacio>, EspacioMemoryRepository>();
builder.Services.AddSingleton<EspacioLogic>();
builder.Services.AddSingleton<Persistencia>();

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
