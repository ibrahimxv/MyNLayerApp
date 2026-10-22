using AutoMapper;
using MyNLayerApp.Business.Dtos.DepartmentDtos;
using MyNLayerApp.Entity.Entities;

namespace MyNLayerApp.Business.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Department, DepartmentReturnDto>();
        CreateMap<DepartmentCreateDto, Department>();
    }
}


//public static Department DepartmentCreateDtoToDepartment(DepartmentCreateDto departmentCreateDto)
//    {
//        return new Department
//        {
//            Name = departmentCreateDto.Name,
//            Description = departmentCreateDto.Description,
//            Location = departmentCreateDto.Location,
//            Limit = departmentCreateDto.Limit
//        };
//    }
//    public static DepartmentReturnDto DepartmentToDepartmentReturnDto(Department department)
//    {
//        return new DepartmentReturnDto
//        {
//            Id = department.Id,
//            Name = department.Name,
//            Description = department.Description,
//            Location = department.Location,
//            Limit = department.Limit,
//            CreatedAt = department.CreatedAt,
//            UpdatedAt = department.UpdatedAt,
//            Employees = department.Employees.Select(e => new EmployeeInDepartmentReturnDto
//            {
//                FirstName = e.FirstName,
//                LastName = e.LastName,
//                Email = e.Email,
//                PhoneNumber = e.PhoneNumber,
//                HireDate = e.HireDate,
//                Salary = e.Salary
//            }
//            ).ToList()
//        };
//    }

//    public static Department DepartmentReturnDtoToDepartment(DepartmentReturnDto departmentReturnDto)
//    {
//        return new Department
//        {
//            Id = departmentReturnDto.Id,
//            Name = departmentReturnDto.Name,
//            Description = departmentReturnDto.Description,
//            Location = departmentReturnDto.Location,
//            Limit = departmentReturnDto.Limit,
//            CreatedAt = departmentReturnDto.CreatedAt,
//            UpdatedAt = departmentReturnDto.UpdatedAt
//        };
//    }