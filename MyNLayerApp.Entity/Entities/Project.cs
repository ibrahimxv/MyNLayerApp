using MyNLayerApp.Entity.Entities.Common;

namespace MyNLayerApp.Entity.Entities;

public class Project : AuditEntity
{
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
    public List<EmployeeProject> EmployeeProjects { get; set; }
    public Project()
    {
        EmployeeProjects = new List<EmployeeProject>();
    }
}
