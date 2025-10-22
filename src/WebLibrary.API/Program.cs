using Microsoft.AspNetCore.Diagnostics;
using Scalar.AspNetCore;
using WebLibrary.API.ExceptionHandlers;
using WebLibrary.BLL;
using WebLibrary.DAL;

namespace WebLibrary.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();
        
        builder.Services.AddServices();
        builder.Services.AddRepositories();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseExceptionHandler(opt => {});
        app.MapControllers();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.Run();
    }
}