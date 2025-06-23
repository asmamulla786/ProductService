using Microsoft.EntityFrameworkCore;
using ProductWithCleanArch.Domain.Entities;

namespace ProductWithCleanArch.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Product> Products { get; set; }
}