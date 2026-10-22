using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyNLayerApp.Entity.Entities;
using MyNLayerApp.Entity.Entities.Common;

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
    public override int SaveChanges()
    {
        var datas = ChangeTracker.Entries<AuditEntity>().ToList();
        foreach (var data in datas)
        {
            switch (data.State)
            {
                case EntityState.Added:
                    data.Entity.CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    data.Entity.UpdatedAt = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChanges();
    }
}
