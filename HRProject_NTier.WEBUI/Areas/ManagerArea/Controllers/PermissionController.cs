using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class PermissionController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPersonnelRepository _personnelRepository;

        public PermissionController(IPermissionRepository permissionRepository, IPersonnelRepository personnelRepository)
        {

            _permissionRepository = permissionRepository;
            _personnelRepository = personnelRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowPermission()
        {
            return View();
        }
        public IActionResult DefinePermission()
        {
            return View();
        }
        public IActionResult InsertPermission()
        {
            return View();
        }
    }
}
