using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.IKEA.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _enviroment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment enviroment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _enviroment = enviroment;
        }

        [HttpGet] // GET: "BaseUrl/Departemnts/Index"
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        [HttpGet] // GET: "BaseUrl/Department/Create"
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST : "BaseUrl/Department/Create"
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            try
            {
               var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Department is not Created");
                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_enviroment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Department is not Created");

            }

            return View(department);
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentDetails(id.Value);

            if(department is { })
                return View(department);

            return NotFound();
        }

    }
}
