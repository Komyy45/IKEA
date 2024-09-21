using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Common.Enums;
using Linkdev.IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Linkdev.IKEA.DAL.Presistance.Data.Configurations.Employees
{
	internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Id)
				   .UseIdentityColumn(1,1);

			builder.Property(E => E.Name)
				   .HasColumnType(nameof(SqlDbType.VarChar))
				   .HasMaxLength(50);

			builder.Property(E => E.Address)
				   .HasColumnType(nameof(SqlDbType.VarChar))
				   .HasMaxLength(100);

			builder.Property(E => E.Salary)
				   .HasColumnType("decimal(18,2)");

			builder.Property(E => E.CreatedOn)
				   .HasDefaultValueSql("GetUtcDate()");

			builder.Property(E => E.Gender)
				   .HasConversion(
					(gender) => gender.ToString(),
					(genderAsString) => (Gender) Enum.Parse(typeof(Gender), genderAsString)
					);

			builder.Property(E => E.EmployeeType)
				   .HasConversion(
					(EmployeeType) => EmployeeType.ToString(),
					(EmployeeTypeAsString) => (EmployeeType) Enum.Parse(typeof(EmployeeType), EmployeeTypeAsString)
					);
		}
	}
}
