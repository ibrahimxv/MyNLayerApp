using Microsoft.EntityFrameworkCore;
using MyNLayerApp.Business.Services.Interfaces;
using MyNLayerApp.DataAccess.Contexts;
using MyNLayerApp.Entity.Entities;

namespace MyNLayerApp.Business.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly MyNLayerAppContext _context;
    public EmployeeService(MyNLayerAppContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync() =>
        await _context.Employees
        .Include(e => e.Department)
        .ToListAsync();

    public async Task<Employee> GetEmployeeByIdAsync(int? id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null) throw new KeyNotFoundException($"Employee with id {id} not found.");

        return employee;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        var department = await _context.Departments
            .Where(d => d.Id == employee.DepartmentId)
            .Select(d => new
            {
                d.Limit,
                EmployeeCount = d.Employees.Count
            })
            .SingleOrDefaultAsync();

        if (department is null)
            throw new InvalidOperationException(
                $"Department with id {employee.DepartmentId} does not exist.");

        if (department.EmployeeCount >= department.Limit)
            throw new InvalidOperationException(
                $"Department with id {employee.DepartmentId} has reached its employee limit.");

        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == employee.Email);

        if (existingEmployee is not null)
            throw new InvalidOperationException(
                $"Employee with email {employee.Email} already exists.");

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> UpdateEmployeeAsync(Employee employee)
    {
        if (employee is null) throw new ArgumentNullException(nameof(employee));

        var existingEmployee = await GetEmployeeByIdAsync(employee.Id);
        var duplicateEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == employee.Email);

        if (duplicateEmployee is not null)
            throw new InvalidOperationException($"Employee with email {employee.Email} not found.");

        if (existingEmployee.DepartmentId != employee.DepartmentId)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == employee.DepartmentId);

            if (department is null)
                throw new InvalidOperationException
                    ($"Department with id {employee.DepartmentId} does not exists.");

            if (department.Employees.Count >= department.Limit)
                throw new InvalidOperationException
                    ($"Department with id {employee.DepartmentId} has reached the limit.");
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.PhoneNumber = employee.PhoneNumber;
        existingEmployee.HireDate = employee.HireDate;
        existingEmployee.Salary = employee.Salary;
        existingEmployee.Status = employee.Status;
        existingEmployee.DepartmentId = employee.DepartmentId;
        existingEmployee.UpdatedAt = employee.UpdatedAt;

        await _context.SaveChangesAsync();

        return existingEmployee;
    }

    public async Task DeleteEmployeeAsync(int? id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
