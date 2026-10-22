using MyNLayerApp.Business.Dtos.DepartmentDtos;
using MyNLayerApp.Entity.Entities;

namespace MyNLayerApp.Business.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department> CreateDepartmentAsync(DepartmentCreateDto departmentCreateDto);
        Task DeleteDepartmentAsync(int? id);
        Task<List<DepartmentReturnDto>> GetAllDepartmentsAsync();
        Task<DepartmentReturnDto> GetByIdAsync(int? id);
        Task<Department> UpdateDepartmentAsync(Department department);
    }
}