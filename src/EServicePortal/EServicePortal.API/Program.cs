
using EServicePortal.API.Extensions;
using EServicePortal.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.ConfigureApiPipeline();

app.Run();

public partial class Program;
