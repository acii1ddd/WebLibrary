using Microsoft.Extensions.DependencyInjection;
using WebLibrary.DAL.Interfaces;
using WebLibrary.DAL.Repositories;

namespace WebLibrary.DAL;

public static class ConfigurationExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
    }
}