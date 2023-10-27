using WebApi;
using WebApi.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInjection();
builder.Services.AddDatabase();
var app = builder.Build();
app.AddMiddleWare();
app.MapGet("/", () => builder.Environment);
app.MapControllers();
app.Run();