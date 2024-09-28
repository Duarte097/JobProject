using ApiCrud.Data;
using ApiCrud.Documentos;
using ApiCrud.Permisao;
using ApiCrud.Usuario;
using ApiCrud.VersaoDoc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; // Adicione esta linha

// Add necessary services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddCors();
builder.Services.AddControllers();

// JWT configuration for authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "FallbackSecretKeyWithAtLeast32Characters");


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
       ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero 
    };
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure authentication and authorization
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Top-level route registrations for controllers
app.MapControllers();

// Add custom route registrations if necessary
//app.AddRotasDocumentos();
//app.DocumentosController();
app.AddRotasUsuarios();
//app.AddRotasPermissao();
//app.AddRotasVersaoDoc();
app.AddRotasLogin(); // Uncomment if using login

app.Run();