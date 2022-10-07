using Microsoft.AspNetCore.Mvc;
using QulixTestTask.Models;
using QulixTestTask.Repositories;

namespace QulixTestTask.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;

        public HomeController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            this.employeeRepository = employeeRepository;
            this.companyRepository = companyRepository;
        }

        /// <summary>
        /// Gets and shows all the employees
        /// </summary>
        /// <returns> List<Employees> </returns>
        public IActionResult Default()
        {
            var employees = employeeRepository.GetEmployees();
            return View(employees);
        }

        /// <summary>
        /// Shows creating employee form
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var companies = companyRepository.GetCompanies();
            return View(companies);
        }

        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            employeeRepository.CreateEmployee(employee);
            return RedirectToAction("Default");
        }

        /// <summary>
        /// Shows editing employee form
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IActionResult Edit(int employeeId)
        {
            var employee = employeeRepository.GetEmployeeById(employeeId);
            var companies = companyRepository.GetCompanies();
            ViewBag.Companies = companies;
            return View(employee);
        }

        /// <summary>
        /// Edit current employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Default");
        }

        /// <summary>
        /// Remove employee by EmployeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IActionResult Remove(int employeeId)
        {
            employeeRepository.DeleteEmployee(employeeId);
            return RedirectToAction("Default");
        }
    }
}
