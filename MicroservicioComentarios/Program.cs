using Application.Interfaces;
using Application.UseCases.SComentario;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<MicroservicioComentarioContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IComentarioCommand, ComentarioCommand>();
builder.Services.AddScoped<IComentarioQuery, ComentarioQuery>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();


//CORS deshabilitar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// (descomentar luego) agregado servicio de token

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtBearerOptions =>
//{
//    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
//    {
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])
//        ),
//        ValidIssuer = "localhost",
//        ValidAudience = "usuarios",
//        ValidateLifetime = true
//    };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
