using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebLibrary.API.ExceptionHandlers;
using WebLibrary.API.Extensions;
using WebLibrary.BLL;
using WebLibrary.DAL;

namespace WebLibrary.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        
        builder.Services.AddServices();
        builder.Services.AddRepositories();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("The default connection string is null.");

        builder.Services.AddDbContext<LibraryContext>(opt
            => opt.UseSqlServer(connectionString));
        
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();

            await app.ApplyMigrationsAsync();
        }

        app.UseExceptionHandler(opt => {});
        app.MapControllers();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        await app.RunAsync();
    }
}