using WebLibrary.DAL;

namespace WebLibrary.API.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        
        await DataInitializer.Initialize(context);
    }
}