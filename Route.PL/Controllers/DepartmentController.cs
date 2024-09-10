using Microsoft.AspNetCore.Mvc;
using Route.BLL.Interfaces;
using Route.DAL.Entities;

namespace Route.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(
            /*IDepartmentRepository departmentRepository,*/ 
            ILogger<DepartmentController> logger,
            IUnitOfWork unitOfWork)
            
        {
            //_departmentRepository = departmentRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View(new Department());
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                return RedirectToAction("Index");
            }
            else
                return View(department);
        }

        public IActionResult Details(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return View(department);
        }

        public IActionResult Update(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return View(department);
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Update(department);
                return RedirectToAction("Index");
            }
            else
                return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            _unitOfWork.DepartmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
    }
}
