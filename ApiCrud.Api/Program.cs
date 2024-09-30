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
var configuration = builder.Configuration; 


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


var app = builder.Build();


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


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


//app.AddRotasDocumentos();
//app.DocumentosController();
app.AddRotasUsuarios();
//app.AddRotasPermissao();
//app.AddRotasVersaoDoc();
app.AddRotasLogin(); 

app.Run();