using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{

    [Area("ManagerArea")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPackageRepository _packageRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPersonnelRepository _personnelRepository;

        public HomeController(ILogger<HomeController> logger, IPackageRepository packageRepository, IPersonnelRepository personnelRepository, IPermissionRepository permissionRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
            _permissionRepository = permissionRepository;
            _personnelRepository = personnelRepository;
        }



        //PaketList
        
        public IActionResult Index()
        {

            //ViewData["UpComingBirthdays"] = _personnelRepository.Personnels.Where(x => x.BirthDate.Month == DateTime.Now.Month && x.BirthDate.Day - DateTime.Now.Day <= 3).ToList();
            //ViewData["UpComingPermissions"] = _permissionRepository.Permissions.Where(x => x.StartedDate.Month == DateTime.Now.Month && x.StartedDate.Day - DateTime.Now.Day <= 3).Include(x => x.Personnel).ToList();

            return View(Tuple.Create<IEnumerable<Package>, IEnumerable<Permission>, IEnumerable<Personnel>>(_packageRepository.Packages.ToList(), _permissionRepository.Permissions.Where(x => x.StartedDate.Month == DateTime.Now.Month && x.StartedDate.Day - DateTime.Now.Day <= 3 && x.StartedDate.Day-DateTime.Now.Day>0).Include(x => x.Personnel).ToList()
                           , _personnelRepository.Personnels.Where(x => x.BirthDate.Month == DateTime.Now.Month && x.BirthDate.Day - DateTime.Now.Day <= 3 && x.BirthDate.Day - DateTime.Now.Day > 0).ToList()));

        }
        [HttpPost]
        public IActionResult Index([Bind(Prefix = "item1")] IEnumerable<Package> item1, [Bind(Prefix = "item2")] IEnumerable<Permission> item2, [Bind(Prefix = "item3")] IEnumerable<Personnel> item3)
        {
            return View();
        }

    }
}
