
using AdminPortal.API.Extensions;
using AdminPortal.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.ConfigureApiPipeline();

app.Run();

public partial class Program;
