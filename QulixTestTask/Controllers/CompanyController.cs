using Microsoft.AspNetCore.Mvc;
using QulixTestTask.Models;
using QulixTestTask.Repositories;
using System.Collections.Generic;

namespace QulixTestTask.Controllers
{
    public class CompanyController : Controller
    {
        private ICompanyRepository companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        /// <summary>
        /// Gets and shows all the companies
        /// </summary>
        /// <returns> List<Employees> </returns>
        public IActionResult Default()
        {
            var companies = companyRepository.GetCompanies();
            return View(companies ?? new List<Company>());
        }

        /// <summary>
        /// Shows creating company form
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Company company)
        {
            companyRepository.CreateCompany(company);
            return RedirectToAction("Default");
        }

        /// <summary>
        /// Shows editing company form
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IActionResult Edit(int companyId)
        {
            var company = companyRepository.GetCompanyById(companyId);
            return View(company);
        }

        /// <summary>
        /// Edit company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Company company)
        {
            companyRepository.UpdateCompany(company);
            return RedirectToAction("Default");
        }

        /// <summary>
        /// Remove company by CompanyId
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public IActionResult Remove(int companyId)
        {
            companyRepository.DeleteCompany(companyId);
            return RedirectToAction("Default");
        }
    }
}
