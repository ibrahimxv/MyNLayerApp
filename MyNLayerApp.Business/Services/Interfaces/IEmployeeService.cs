using MyNLayerApp.Entity.Entities;

namespace MyNLayerApp.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int? id);
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int? id);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}