using Microsoft.Extensions.DependencyInjection;
using WebLibrary.BLL.Interfaces;
using WebLibrary.BLL.Services;

namespace WebLibrary.BLL;

public static class ConfigurationExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
    }
}