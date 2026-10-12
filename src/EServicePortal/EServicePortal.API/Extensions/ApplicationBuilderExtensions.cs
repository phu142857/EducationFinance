
namespace EServicePortal.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication ConfigureApiPipeline(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}
