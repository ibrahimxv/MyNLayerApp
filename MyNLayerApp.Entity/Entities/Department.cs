using MyNLayerApp.Entity.Entities.Common;

namespace MyNLayerApp.Entity.Entities;

public class Department : AuditEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Location { get; set; }
    public int Limit { get; set; }

    public List<Employee> Employees { get; set; }
    public Department()
    {
         Employees = new List<Employee>();
    }
}
