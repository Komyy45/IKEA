namespace Linkdev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateOnly CreationDate { get; set; }
    }
}
