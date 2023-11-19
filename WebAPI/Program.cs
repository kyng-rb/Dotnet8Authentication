using Carter;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication;
using WebAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services
       .AddDbContext<ApplicationContext>(context => context.UseSqlite("DataSource=app.db"));

builder.Services.AddCarter();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();
app.Run();