using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductWithCleanArch.Application.Mappings;
using ProductWithCleanArch.Application.Products.Queries;
using ProductWithCleanArch.Domain.Interfaces;
using ProductWithCleanArch.Infrastructure.Data;
using ProductWithCleanArch.Infrastructure.Repositories;

namespace ProductWithCleanArch.API;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateApp(args);
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
        
    }

    private static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        if (!builder.Environment.IsEnvironment("Testing"))
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
        builder.Services.AddMediatR(typeof(GetProductsQuery).Assembly);
        var app = builder.Build();
        return app;
    }
}