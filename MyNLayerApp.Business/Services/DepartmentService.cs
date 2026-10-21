namespace MyNLayerApp.Business.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyNLayerApp.DataAccess.Contexts;
using MyNLayerApp.Entity.Entities;

public class DepartmentService
{
    private readonly MyNLayerAppContext _context;
    public DepartmentService(MyNLayerAppContext context)
    {
        _context = context;
    }
    public async Task<Department> CreateAsync(Department department)
    {
        if (department is null) throw new ArgumentNullException(nameof(department));
        var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name == department.Name);
        if (existingDepartment != null) throw new InvalidOperationException($"Department with name {department.Name} already exists.");
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<List<Department>>GetAllAsync()=>
        await _context.Departments.ToListAsync();

    public async Task<Department?> GetByIdAsync(int? id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        var department = await _context.Departments.FindAsync();
        if (department is null) throw new KeyNotFoundException($"Department with id {id} not found.");
        return department;
    }

    public async Task<Department> UpdateAsync (Department department)
    {
        var existingDepartment = await GetByIdAsync(department.Id);

        if (existingDepartment is null) throw new KeyNotFoundException($"Department with id {department.Id} not found.");
        var duplicateDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name == department.Name && d.Id != department.Id);
        if (duplicateDepartment != null) throw new InvalidOperationException($"Department with name {department.Name} already exists.");

        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;

        await _context.SaveChangesAsync();
        return existingDepartment;
    }

    public async Task DeleteAsync (int? id)
    {
        var department = await GetByIdAsync(id);

        if (department is null)
            throw new KeyNotFoundException("Department not found.");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}
