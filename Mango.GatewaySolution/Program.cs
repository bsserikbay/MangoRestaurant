using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7046";
        options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
    });
builder.Services.AddOcelot();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
await app.UseOcelot();

app.Run();