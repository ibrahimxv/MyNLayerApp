using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyNLayerApp.Entity.Entities;

namespace MyNLayerApp.DataAccess.Contexts;

public class MyNLayerAppContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<EmployeeProject> EmployeeProjects { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=IBO\\SQLEXPRESS;Database=MyNLayerAppDb;TrustServerCertificate=true;Trusted_Connection=True");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyNLayerAppContext).Assembly);
    }
}
