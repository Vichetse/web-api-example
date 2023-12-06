using WebApi;
using WebApi.Core;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInjection();
builder.Services.AddDatabase();
builder.Services.AddControllers();

var app = builder.Build();

app.AddMiddleWare();
app.MapGet("/", () => builder.Environment);
app.MapControllers();
app.Run();