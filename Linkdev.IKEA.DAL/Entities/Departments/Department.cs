using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities.Employees;

namespace Linkdev.IKEA.DAL.Entities.Departments
{
    public class Department : ModelBase
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
