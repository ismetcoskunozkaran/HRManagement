using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class CompanyController : Controller
    {

        private readonly ICompanyRepository _companyrepository;
        private readonly IWebHostEnvironment _environment;

        public CompanyController(ICompanyRepository companyRepository, IWebHostEnvironment environment)
        {
            _companyrepository = companyRepository;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateCompany(int id)
        {
            Company company = _companyrepository.GetByID(id);
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCompany(Company company)
        {


            if (company.LogoImageFile != null)
            {

                string resimler = Path.Combine(_environment.WebRootPath, "resimler");

                if (company.LogoImageFile.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(resimler, company.LogoImageFile.FileName), FileMode.Create))
                    {
                        company.LogoImageFile.CopyTo(fileStream);
                    }
                }
                else if (company.LogoImageFile.Length < 1000)
                {

                }
                company.LogoImage = company.LogoImageFile.FileName;
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _companyrepository.UpdateCompany(company);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return View(company);
        }

    }
}
