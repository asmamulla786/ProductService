using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductWithCleanArch.Domain.Entities;
using ProductWithCleanArch.Infrastructure.Data;

namespace ProductWithCleanArch.Tests.Integration;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing"); 
        builder.ConfigureServices(services =>
        {
            // Remove original db
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add InMemory DB
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            db.Products.Add(new Product{Id = Guid.NewGuid(), Name = "Book", Price = 100});
            db.Products.Add(new Product{Id = Guid.NewGuid(), Name = "Pen", Price = 10});
            db.SaveChanges();
        });
    }
}
