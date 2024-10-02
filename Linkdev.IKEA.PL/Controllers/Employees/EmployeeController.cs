using AutoMapper;
using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.BLL.Services.Employees;
using Linkdev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.IKEA.PL.Controllers.Employees
{
	[Authorize]
    public class EmployeeController : Controller
	{
		#region Services

		private readonly IEmployeeService _employeeService;
		private readonly ILogger<EmployeeController> _logger;
		private readonly IWebHostEnvironment _enviroment;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService,
									ILogger<EmployeeController> logger,
									IWebHostEnvironment enviroment,
									IMapper mapper)
		{
			_employeeService = employeeService;
			_logger = logger;
			_enviroment = enviroment;
            _mapper = mapper;
        }

		#endregion

		#region Index

		[HttpGet] // GET: "BaseUrl/Employee/Index"
		public async Task<IActionResult> Index()
		{
			var employees = await _employeeService.GetEmployeesAsync();

			return View(employees);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string searchValue)
		{
            var employees = await _employeeService.GetEmployeesAsync(searchValue);

            return PartialView("Partials/_EmployeeListPartial", employees);
        }

		#endregion

		#region Create

		[HttpGet] // GET: "BaseUrl/Employee/Create"
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost] // POST : "BaseUrl/Employee/Create"
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
		{
            if (!ModelState.IsValid)
				return View(employee);

			var message = "Employee is not Created";

			var newEmployee = _mapper.Map<CreatedEmployeeDto>(employee);

			try
			{
				var result = await _employeeService.CreateEmployeeAsync(newEmployee);

				if (result > 0)
					return RedirectToAction(nameof(Index));

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				if (_enviroment.IsDevelopment())
					message = ex.Message;
			}

			ModelState.AddModelError(string.Empty, message);
			return View(employee);
		}

		#endregion

		#region Details

		[HttpGet] // GET : "BaseUrl/Employee/Details/id?"
		public async Task<IActionResult> Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var employee = await _employeeService.GetEmployeeDetailsAsync(id.Value);

			if (employee is { })
				return View(employee);

			return NotFound();
		}

		#endregion

		#region Edit

		[HttpGet] // GET : "BaseUrl/Employee/Edit/id?"
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null)
				return BadRequest();

			var employee = await _employeeService.GetEmployeeDetailsAsync(id.Value);

			var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

			if (employee is { })
				return View(employeeViewModel);

			return NotFound();
		}

		[HttpPost] // POST: "BaseUrl/Employee/Edit/id?"
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employee)
		{
			if (id is null)
				return BadRequest();

			if (!ModelState.IsValid)
				return View(employee);

			var message = "Employee isn't Created";

			var UpdatedEmployee = _mapper.Map<UpdatedEmployeeDto>(employee, (context) => context.Items["Id"] = $"{id}");

			try
			{
				var IsUpdated = await _employeeService.UpdateEmployeeAsync(UpdatedEmployee) > 0;

				if (IsUpdated)
					return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. Log Exception
				_logger.LogError(ex, ex.Message);

				// 2. Set Friendly Message
				if (_enviroment.IsDevelopment())
					message = ex.Message;
			}

			ModelState.AddModelError(string.Empty, message);
			return View(employee);
		}

		#endregion

		#region Delete

		[HttpGet] // GET : "BaseUrl/Employee/Delete/id?"
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();

			var employee = await _employeeService.GetEmployeeDetailsAsync(id.Value);

			if (employee is { })
				return View(employee);


			return NotFound();
		}

		[HttpPost] // POST : "BaseUrl/Employee/Delete/id"
        public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
				return BadRequest();

			var message = "An Error Has Been Occured!, Please Try Again later";

			try
			{
				var IsDeleted = await _employeeService.DeleteEmployeeAsync(id);

				if (IsDeleted) return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// 1. log Error
				_logger.LogError(ex, ex.Message);

				// 2. Friendly Message
				if(_enviroment.IsDevelopment())
				message =  ex.Message;
			}

			ModelState.AddModelError(string.Empty, message);
			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
