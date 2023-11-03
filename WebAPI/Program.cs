using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<MyUser>()
       .AddEntityFrameworkStores<ApplicationContext>();

builder.Services
       .AddDbContext<ApplicationContext>(context => context.UseSqlite("DataSource=app.db"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/Fake/Authenticated", () => "Hello World!").RequireAuthorization();
app.MapGet("/Fake/AuthenticatedWithClaims", 
           (ClaimsPrincipal user) => $@"Fuck you {user.Identity?.Name} - You are 
                                               Authenticated with ${user.Identity?.AuthenticationType}
                                               Your claims are {string.Join("-", user.Claims)}
                                               Your Identity are {string.Join("-", user.Identities.Select(x => x))}")
    .RequireAuthorization();
app.MapGet("/Fake/NoAuthenticated", () => "Hello World!");
app.MapGet("/Fake/Anonymous", () => "Hello World!").AllowAnonymous();

app.MapGroup("account/").MapIdentityApi<MyUser>();

app.Run();