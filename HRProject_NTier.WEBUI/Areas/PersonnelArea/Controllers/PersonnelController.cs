using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.PersonnelArea.Controllers
{
    [Area("PersonnelArea")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelRepository _personnelrepository;
        private readonly IPermissionRepository _permissionRepository;
        public PersonnelController(IPersonnelRepository personnelrepository, IPermissionRepository permissionRepository)
        {
            _personnelrepository = personnelrepository;
            _permissionRepository = permissionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpdatePersonnel(int id) => View(_personnelrepository.GetByID(id));
        [HttpPost]
        public IActionResult UpdatePersonnel(Personnel personnel)
        {
            if (personnel.ID == Convert.ToInt32(HttpContext.Session.GetInt32("id")))
            {
                _personnelrepository.UpdatePersonnel(personnel);
            }
            //personnel.ID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
           // _personnelrepository.UpdatePersonnel(personnel);
           
            return RedirectToAction("Index", "Home", new { area = "PersonnelArea" });

        }

    

      
    }
}
