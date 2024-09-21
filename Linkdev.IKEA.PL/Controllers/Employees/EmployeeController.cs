using Linkdev.IKEA.BLL.Models.Employees;
using Linkdev.IKEA.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.IKEA.PL.Controllers.Employees
{
	public class EmployeeController : Controller
	{
		#region Services

		private readonly IEmployeeService _employeeService;
		private readonly ILogger<EmployeeController> _logger;
		private readonly IWebHostEnvironment _enviroment;

		public EmployeeController(IEmployeeService employeeService,
									ILogger<EmployeeController> logger,
									IWebHostEnvironment enviroment)
		{
			_employeeService = employeeService;
			_logger = logger;
			_enviroment = enviroment;
		}

		#endregion

		#region Index

		[HttpGet] // GET: "BaseUrl/Employee/Index"
		public IActionResult Index()
		{
			var employees = _employeeService.GetAllEmployees();

			return View(employees);
		}

		#endregion

		#region Create

		[HttpGet] // GET: "BaseUrl/Employee/Create"
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost] // POST : "BaseUrl/Employee/Create"
		public IActionResult Create(CreatedEmployeeDto employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			var message = "Employee is not Created";

			try
			{
				var result = _employeeService.CreateEmployee(employee);

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
		public IActionResult Details(int? id)
		{
			if (id is null)
				return BadRequest();

			var employee = _employeeService.GetEmployeeDetails(id.Value);

			if (employee is { })
				return View(employee);

			return NotFound();
		}

		#endregion

		#region Edit

		//[HttpGet] // GET : "BaseUrl/Employee/Edit/id?"
		//public IActionResult Edit(int? id)
		//{
		//	if (id is null)
		//		return BadRequest();

		//	var employee = _employeeService.GetEmployeeDetails(id.Value);

		//	if (employee is { })
		//		return View(new EmployeeEditViewModel()
		//		{

		//		});

		//	return NotFound();
		//}

		//[HttpPost] // POST: "BaseUrl/Employee/Edit/id?"
		//public IActionResult Edit([FromRoute] int? id, EmployeeEditViewModel employee)
		//{
		//	if (id is null)
		//		return BadRequest();

		//	if (!ModelState.IsValid)
		//		return View(employee);

		//	var updatedEmployee = new UpdatedEmployeeDto()
		//	{
		//		Id = id.Value,
		//		Name = employee.Name,
		//		Code = employee.Code,
		//		Description = employee.Description,
		//		CreationDate = employee.CreationDate,
		//	};

		//	var message = "Employee isn't Created";

		//	try
		//	{
		//		var IsUpdated = _employeeService.UpdateEmployee(updatedEmployee) > 0;

		//		if (IsUpdated)
		//			return RedirectToAction(nameof(Index));
		//	}
		//	catch (Exception ex)
		//	{
		//		// 1. Log Exception
		//		_logger.LogError(ex, ex.Message);

		//		// 2. Set Friendly Message
		//		if (_enviroment.IsDevelopment())
		//			message = ex.Message;
		//	}

		//	ModelState.AddModelError(string.Empty, message);
		//	return View(employee);
		//}

		#endregion

		#region Delete

		[HttpGet] // GET : "BaseUrl/Employee/Delete/id?"
		public IActionResult Delete(int? id)
		{
			if (id is null) return BadRequest();

			var employee = _employeeService.GetEmployeeDetails(id.Value);

			if (employee is { })
				return View(employee);


			return NotFound();
		}

		[HttpPost] // POST : "BaseUrl/Employee/Delete/id"
		public IActionResult Delete(int id)
		{
			if (id == 0)
				return BadRequest();

			var message = "An Error Has Been Occured!, Please Try Again later";

			try
			{
				var IsDeleted = _employeeService.DeleteEmployee(id);

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
