using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<MyUser>()
       .AddEntityFrameworkStores<ApplicationContext>()
       .AddApiEndpoints();

builder.Services
       .AddDbContext<ApplicationContext>(context => context.UseSqlite("DataSource=app.db"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapIdentityApi<MyUser>();

app.Run();