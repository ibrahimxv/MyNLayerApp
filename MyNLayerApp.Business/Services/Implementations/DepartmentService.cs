namespace MyNLayerApp.Business.Services.Implementations;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyNLayerApp.Business.Dtos.DepartmentDtos;
using MyNLayerApp.Business.Mappers;
using MyNLayerApp.Business.Services.Interfaces;
using MyNLayerApp.DataAccess.Contexts;
using MyNLayerApp.Entity.Entities;

public class DepartmentService : IDepartmentService
{
    private readonly MyNLayerAppContext _context;
    private readonly IMapper _mapper;
    public DepartmentService(MyNLayerAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Department> CreateDepartmentAsync(DepartmentCreateDto departmentCreateDto)
    {
        if (departmentCreateDto is null) 
            throw new ArgumentNullException(nameof(departmentCreateDto));

        var existingDepartment = await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == departmentCreateDto.Name);

        if (existingDepartment != null) 
            throw new InvalidOperationException($"Department with name {departmentCreateDto.Name} already exists.");

        var department = _mapper.Map<Department>(departmentCreateDto);

        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
        return department;
    }

    public async Task<List<DepartmentReturnDto>> GetAllDepartmentsAsync() =>
        await _context.Departments
        .AsNoTracking()
        .Select(d =>  _mapper.Map<DepartmentReturnDto> (d))
        .ToListAsync();

    public async Task<DepartmentReturnDto> GetByIdAsync(int? id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        var department = await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
        if (department is null) throw new KeyNotFoundException($"Department with id {id} not found.");
        var departmentReturnDto = _mapper.Map<DepartmentReturnDto>(department);

        return departmentReturnDto;
    }

    public async Task<Department> UpdateDepartmentAsync(Department department)
    {
        var existingDepartment = await _context.Departments.FindAsync(department.Id);

        if (existingDepartment is null) throw new KeyNotFoundException($"Department with id {department.Id} not found.");
        var duplicateDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Name == department.Name && d.Id != department.Id);
        if (duplicateDepartment != null) throw new InvalidOperationException($"Department with name {department.Name} already exists.");

        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;

        await _context.SaveChangesAsync();
        return existingDepartment;
    }

    public async Task DeleteDepartmentAsync(int? id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        var department = await _context.Departments.FindAsync(id.Value);

        if (department is null)
            throw new KeyNotFoundException($"Department with id {id} not found.");

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
    }
}
