using MyNLayerApp.Entity.Entities.Common;
using MyNLayerApp.Entity.Enums;

namespace MyNLayerApp.Entity.Entities;

public class Employee : AuditEntity
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public EmployeeStatus Status { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public List<EmployeeProject> EmployeeProjects { get; set; }
    public Employee()
    {
        EmployeeProjects = new List<EmployeeProject>();
    }
}
