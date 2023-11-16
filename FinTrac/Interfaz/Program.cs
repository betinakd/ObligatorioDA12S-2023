using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Repositorio;
using Dominio;
using LogicaNegocio;
using Controlador;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FintracDbContext>
	(options => options.UseSqlServer
	(builder.Configuration.GetConnectionString("FintracsDbConection"),
		providerOptions => providerOptions.EnableRetryOnFailure()));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IRepositorio<Usuario>, UsuarioMemoriaRepositorio>();
builder.Services.AddScoped<UsuarioLogica>();
builder.Services.AddScoped<IRepositorio<Espacio>, EspacioMemoriaRepositorio>();
builder.Services.AddScoped<EspacioLogica>();
builder.Services.AddSingleton<Persistencia>();
builder.Services.AddMudServices();
builder.Services.AddScoped<ControladorRegistro>();
builder.Services.AddScoped<ControladorUsuarios>();
builder.Services.AddScoped<ControladorEspacios>();
builder.Services.AddScoped<ControladorCuenta>();
builder.Services.AddScoped<ControladorTransaccion>();
builder.Services.AddScoped<ControladorHome>();
builder.Services.AddScoped<ControladorObjetivos>();
builder.Services.AddScoped<ControladorCategorias>();
builder.Services.AddScoped<ControladorCambios>();
builder.Services.AddScoped<ControladorSesion>();
builder.Services.AddScoped<ControladorReporte>();

builder.Services.AddDbContext<FintracDbContext>
	(options => options.UseSqlServer
	(builder.Configuration.GetConnectionString("FintracsDbConection"),
		providerOptions => providerOptions.EnableRetryOnFailure()));


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