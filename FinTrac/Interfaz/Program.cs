using Repository;
using Domain;
using BussinesLogic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FintracDbContext>
	(options => options.UseSqlServer
	(builder.Configuration.GetConnectionString("DbConection")));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioMemoryRepository>();
builder.Services.AddScoped<UsuarioLogic>();
builder.Services.AddScoped<IRepository<Espacio>, EspacioMemoryRepository>();
builder.Services.AddScoped<EspacioLogic>();
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
