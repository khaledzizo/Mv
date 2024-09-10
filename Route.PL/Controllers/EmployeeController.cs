using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.DAL.Entities;

namespace Route.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string searchValue = "")
        {
            IEnumerable<Employee> employees;
            if (searchValue == "")
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.Search(searchValue);
            
            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(new Employee());
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.EmployeeRepository.Add(employee);
                return RedirectToAction("Index");
            }
            else
                return View(employee);
        }
    }
}
