using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyNLayerApp.Entity.Entities;
using MyNLayerApp.Entity.Enums;

namespace MyNLayerApp.DataAccess.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        builder.HasIndex(e => e.Id);
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.LastName)
            .HasMaxLength(50);
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);
        builder.Property(e => e.HireDate)
            .IsRequired();
        builder.Property(e => e.Salary)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.Property(e => e.Status)
            .IsRequired()
            .HasDefaultValue(EmployeeStatus.Active);

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
