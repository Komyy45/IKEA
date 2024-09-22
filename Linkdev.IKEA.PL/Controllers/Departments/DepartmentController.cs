using Linkdev.IKEA.BLL.Models.Departments;
using Linkdev.IKEA.BLL.Services.Departments;
using Linkdev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Linkdev.IKEA.PL.Controllers.Departments
{
    public class DepartmentController : Controller
    {
        #region Services
        
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _enviroment;

        public DepartmentController(IDepartmentService departmentService,
                                    ILogger<DepartmentController> logger,
                                    IWebHostEnvironment enviroment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _enviroment = enviroment;
        }

        #endregion

        #region Index

        [HttpGet] // GET: "BaseUrl/Departemnts/Index"
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #endregion

        #region Create

        [HttpGet] // GET: "BaseUrl/Department/Create"
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST : "BaseUrl/Department/Create"
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var newDepartment = new CreatedDepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            };

            try
            {
                var result = _departmentService.CreateDepartment(newDepartment);

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

        #endregion

        #region Details
        
        [HttpGet] // GET : "BaseUrl/Department/Details/id?"
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentDetails(id.Value);

            if (department is { })
                return View(department);

            return NotFound();
        }

        #endregion

        #region Edit

        [HttpGet] // GET : "BaseUrl/Department/Edit/id?"
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentDetails(id.Value);

            if (department is { })
                return View(new DepartmentViewModel()
                {
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                });

            return NotFound();
        }

        [HttpPost] // POST: "BaseUrl/Department/Edit/id?"
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, DepartmentViewModel department)
        {
            if (id is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);

            var updatedDepartment = new UpdatedDepartmentDto()
            {
                Id = id.Value,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
            };

            var message = string.Empty;

            try
            {
                var IsUpdated = _departmentService.UpdateDepartment(updatedDepartment) > 0;

                if (IsUpdated)
                    return RedirectToAction(nameof(Index));

                message = "Department isn't Created";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                if (_enviroment.IsDevelopment())
                    message = ex.Message;
                else
                    message = "Department isn't Created";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(department);
        }

        #endregion

        #region Delete

        [HttpGet] // GET : "BaseUrl/Deaprtment/Delete/id?"
        public IActionResult Delete(int? id)
        {
            if(id is null) return BadRequest();

            var department = _departmentService.GetDepartmentDetails(id.Value);

            if(department is { })
                return View(department);
            

            return NotFound();
        }

        [HttpPost] // POST : "BaseUrl/Department/Delete/id"
        public IActionResult Delete(int id)
        {
            if (id == 0) 
                return BadRequest();

            var message = string.Empty;

            try
            {
                var IsDeleted = _departmentService.DeleteDepartment(id);

                if (IsDeleted) return RedirectToAction(nameof(Index));

                message = "An Error Has Been Occured!, Please Try Again later";
            }
            catch (Exception ex)
            {
                // 1. log Error
                _logger.LogError(ex, ex.Message);

                // 2. Friendly Message
                message = _enviroment.IsDevelopment() ? ex.Message : "An Error Has Been Occured!, Please Try Again later";
            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
