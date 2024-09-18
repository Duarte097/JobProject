using ApiCrud.Data;
using ApiCrud.Documentos;
using ApiCrud.Permisao;
using ApiCrud.Usuario;
using ApiCrud.VersaoDoc;
using Microsoft.EntityFrameworkCore;
using System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
//builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.MapGet("Hello-World", () => "Hello World");

//Configurando as rotas
app.AddRotasDocumentos();
app.AddRotasUsuarios();
app.AddRotasPermissao();
app.AddRotasVersaoDoc();
app.Run();


