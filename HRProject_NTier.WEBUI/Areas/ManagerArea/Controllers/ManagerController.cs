using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class ManagerController : Controller
    {
        private IManagerRepository _managerRepository;
        private IPersonnelRepository _personnelRepository;
        private IWebHostEnvironment _managerEnvironment;
        private IPermissionRepository _permissionRepository;
        private IPaymentRepository _paymentRepository;
        private IPackageRepository _packageRepository;
        //private IWebHostEnvironment webHostEnviroment;
        private IWebHostEnvironment _personnelEnvironment;
        private IManagerCommentRepository _managerCommentRepository;
        private ProjectContext _context;
        private ISpendingRepository _spendingRepository;

        public ManagerController(IManagerRepository managerRepository, IWebHostEnvironment managerEnvironment, IPersonnelRepository personnelRepository, IPermissionRepository permissionRepository, IPaymentRepository paymentRepository, IPackageRepository packageRepository, IWebHostEnvironment personelEnvironment, IManagerCommentRepository managerCommentRepository, ProjectContext projectContext, ISpendingRepository spendingRepository)
        {
            _managerRepository = managerRepository;
            _managerEnvironment = managerEnvironment;
            _personnelRepository = personnelRepository;
            _permissionRepository = permissionRepository;
            _paymentRepository = paymentRepository;
            _packageRepository = packageRepository;
            _personnelEnvironment = personelEnvironment;
            _managerCommentRepository = managerCommentRepository;
            _context = projectContext;
            _spendingRepository = spendingRepository;

        }
        public IActionResult Index()
        {
            return View(Tuple.Create<List<Manager>, List<Personnel>, List<Permission>, List<Package>>(_managerRepository.Managers.ToList(), _personnelRepository.Personnels.ToList(), _permissionRepository.Permissions.ToList(), _packageRepository.Packages.ToList()));
        }

        [HttpGet]
        public IActionResult UpdateManager() => View(_managerRepository.GetById(Convert.ToInt32(HttpContext.Session.GetInt32("id"))));


        [HttpPost]

        public IActionResult UpdateManager(Manager manager)
        {

            manager.ID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            manager.IsActived = true;

            if (manager.Password == null || manager.Name == null)
            {

                return View(manager);
            }
            else
            {

                if (manager.ProfileImageFile != null)//Resim boş gelmesi kontrolü
                {
                    //resimyükleme
                    string managerImage = Path.Combine(_managerEnvironment.WebRootPath, "images");
                    if (manager.ProfileImageFile.Length > 0)
                    {
                        using (FileStream file = new FileStream(Path.Combine(managerImage, manager.ProfileImageFile.FileName), FileMode.Create))
                        {
                            manager.ProfileImageFile.CopyTo(file);
                        }
                    }
                    manager.ProfileImage = manager.ProfileImageFile.FileName;
                    
                    _managerRepository.UpdateManager(manager);
                    return RedirectToAction("ManagerPanel", "Manager");
                }
                else
                {
                    _managerRepository.UpdateManager(manager);
                    return RedirectToAction("ManagerPanel", "Manager");
                }
            }

        }
        //Payment
        public IActionResult AddPayment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPayment(InsertPaymentVM paymentVM)
        {
            _paymentRepository.AddPayment(paymentVM);
            ViewData["MesajKaydet"] = "Avans başarı ile kayıt edilmiştir.";
            return RedirectToAction("ListPayment");
        }
        //AvansGüncelleme
        //AvansGüncelleme
        public IActionResult UpdatePayment(Payment payment, int id)
        {
            var result = _paymentRepository.GetById(id);
            if (result.IsActived == false)
            {
                result.IsActived = true;
                result.IsApproved = false;
                _paymentRepository.UpdatePayment(result);
            }
            return RedirectToAction(nameof(ListPayment));
        }

        public IActionResult ReddedilenAvans()
        {
            var result = (from p in _context.Payments
                          join per in _context.Personnels
                          on p.PersonnelID equals per.ID
                          where p.IsApproved == true
                          select new PaymentManagerPersonelVM
                          {
                              PersonelName = per.Name,
                              PersonelSurname = per.Surname,
                              PaymentAmount = p.Amount,
                              Description = p.Description,
                              CreatedDate = p.CreatedDate,
                              IsActived = p.IsActived,
                              IsDeleted = p.IsDeleted,
                              IsApproved = p.IsApproved,
                              PersonelID = per.ID,
                              ID = p.ID
                              //Message=p.Message
                          }).ToList();
            ViewData["reddedilen"] = _paymentRepository.Payments.Where(p => p.IsApproved == true).Count();
            ViewData["onaylanan"] = _paymentRepository.Payments.Where(p => p.IsActived == true).Count();
            ViewData["onaylananList"] = _paymentRepository.Payments.Count();
            ViewData["onaylananBekleyen"] = _paymentRepository.Payments.Where(p => p.IsActived == false && p.IsApproved == false).Count();
            return View(result);
        }

        public IActionResult OnaylananAvans()
        {
            var result = (from p in _context.Payments
                          join per in _context.Personnels
                          on p.PersonnelID equals per.ID
                          where p.IsActived == true
                          select new PaymentManagerPersonelVM
                          {
                              PersonelName = per.Name,
                              PersonelSurname = per.Surname,
                              PaymentAmount = p.Amount,
                              Description = p.Description,
                              CreatedDate = p.CreatedDate,
                              IsActived = p.IsActived,
                              IsDeleted = p.IsDeleted,
                              IsApproved = p.IsApproved,
                              PersonelID = per.ID,
                              ID = p.ID
                              //Message=p.Message
                          }).ToList();
            ViewData["onaylanan"] = _paymentRepository.Payments.Where(p => p.IsActived == true).Count();
            ViewData["reddedilen"] = _paymentRepository.Payments.Where(p => p.IsApproved == true).Count();
            ViewData["onaylananList"] = _paymentRepository.Payments.Count();
            ViewData["onaylananBekleyen"] = _paymentRepository.Payments.Where(p => p.IsActived == false && p.IsApproved == false).Count();
            return View(result);
        }


        //AvansSilme
        public IActionResult DeletePayment(Payment payment, int id)
        {
            var result = _paymentRepository.GetById(id);
            if (result.IsApproved == false)
            {
                result.IsActived = false;
                result.IsApproved = true;
                _paymentRepository.DeletePayment(result);
            }
            return RedirectToAction(nameof(ListPayment));
        }

        //onaybekleyen
        public IActionResult onayBekleyen()
        {
            var result = (from p in _context.Payments
                          join per in _context.Personnels
                          on p.PersonnelID equals per.ID
                          where p.IsActived == false && p.IsApproved == false
                          select new PaymentManagerPersonelVM
                          {
                              PersonelName = per.Name,
                              PersonelSurname = per.Surname,
                              PaymentAmount = p.Amount,
                              Description = p.Description,
                              CreatedDate = p.CreatedDate,
                              IsActived = p.IsActived,
                              IsDeleted = p.IsDeleted,
                              IsApproved = p.IsApproved,
                              PersonelID = per.ID,
                              ID = p.ID
                              //Message=p.Message
                          }).ToList();
            ViewData["onaylananBekleyen"] = _paymentRepository.Payments.Where(p => p.IsActived == false && p.IsApproved == false).Count();
            ViewData["onaylanan"] = _paymentRepository.Payments.Where(p => p.IsActived == true).Count();
            ViewData["reddedilen"] = _paymentRepository.Payments.Where(p => p.IsApproved == true).Count();
            ViewData["onaylananList"] = _paymentRepository.Payments.Count();
            return View(result);
        }
        //AvansListeleme
        public IActionResult ListPayment()
        {
            var result = (from p in _context.Payments
                          join per in _context.Personnels
                          on p.PersonnelID equals per.ID
                          select new PaymentManagerPersonelVM
                          {
                              PersonelName = per.Name,
                              PersonelSurname = per.Surname,
                              PaymentAmount = p.Amount,
                              Description = p.Description,
                              CreatedDate = p.CreatedDate,
                              IsActived = p.IsActived,
                              IsDeleted = p.IsDeleted,
                              IsApproved = p.IsApproved,
                              PersonelID = per.ID,
                              ID = p.ID
                              //Message=p.Message
                          }).ToList();
            ViewData["onaylananList"] = _paymentRepository.Payments.Count();
            ViewData["onaylanan"] = _paymentRepository.Payments.Where(p => p.IsActived == true).Count();
            ViewData["reddedilen"] = _paymentRepository.Payments.Where(p => p.IsApproved == true).Count();
            ViewData["onaylananBekleyen"] = _paymentRepository.Payments.Where(p => p.IsActived == false && p.IsApproved == false).Count();
            return View(result);

        }



        public IActionResult ListPackage()
        {
            List<ListPackageVM> listPackageVMs = _packageRepository
                .Packages
                .Select(x => new ListPackageVM
                {
                    Name = x.Name,
                    PersonnelNumber = x.PersonnelNumber,
                    StartedDate = x.StartedDate,
                    EndDate = x.EndDate,
                    Price = x.Price

                }).ToList();
            return View(listPackageVMs);
        }

        public IActionResult hesabiPasifeAl(int id)
        {

            Manager manager = _managerRepository.GetById(Convert.ToInt32(HttpContext.Session.GetInt32("id")));
            if (manager.IsActived != false)
            {
                manager.IsActived = false;
                _managerRepository.UpdateManager(manager);
            }

            List<Personnel> plist = _personnelRepository.Personnels
                .Where(p => p.ManagerID == Convert.ToInt32(HttpContext.Session.GetInt32("id"))).ToList();
            //List<Personnel> per = plist.Cast<Personnel>().ToList();
            foreach (var item in plist)
            {
                item.IsActived = false;

            }
            _context.Personnels.UpdateRange(plist);
            _context.SaveChanges();

            ViewData["mesaj"] = "Yönetici tarafından, Yönetici ve yöneticiye ait tüm personel hesapları pasif hale geçmiştir.";

            return View(nameof(uyelikAyarlari));
        }
        public IActionResult uyelikAyarlari()
        {
            return View();
        }


        public IActionResult ListPersonnel()
        {

            return View(_personnelRepository.Personnels.Select(x => new InsertPersonnelVM
            {
                ID = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                MailAddress = x.MailAddress,
                Password = x.Password,
                CompanyName = x.CompanyName,
                Department = x.Department,
                ProfileImage = x.ProfileImage,
                //ManagerID= Convert.ToInt32(HttpContext.Session.GetInt32("id"))

            }).ToList());
        }
        //PersonelEkle
        [HttpGet]
        public IActionResult AddPersonnel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPersonnel(InsertPersonnelVM personnelVM)
        {
            
            _personnelRepository.InsertPersonelPhoto(personnelVM);
            return RedirectToAction("ListPersonnel");
        }

        //AktiveİzinListeleme
        public IActionResult ListActivePermissions()
        {
            //ViewData[("PersonelPermissionsDetails")] = _permissionRepository.Permissions.Where(x => x.IsActived == true).Include(x => x.Personnel).ToList();
            ViewData[("PersonelPermissionsDetails")] = _permissionRepository.Permissions.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).ToList();

            ViewBag.BekleyenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();

            return View("ListActivePermissions");

        }

        //İzinTalebiReddedilmişleriListeleme
        [HttpGet]
        public IActionResult ListDissaprovedPermissions()
        {
            ViewData[("PersonelPermissionsDisapprovals")] = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).ToList();

            ViewBag.BekleyenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();
   
            return View("ListDissaprovedPermissions");
     
        }

        //Onaylanan izinlerin listesi
        public IActionResult ListApprovePermissions()
        {
            ViewData[("PersonelPermissionsApprovals")] = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).ToList();

            ViewBag.BekleyenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _permissionRepository.Permissions.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();

            return View("ListApprovePermissions");
        }

        //KişiBazlıPersonelİzinGörüntüleme
        [HttpGet]
        public IActionResult ListPersonnelPermissions(int id)
        {
            ViewData[("PersonelPermissionsDetails")] = _permissionRepository.Permissions.Where(x => x.PersonnelID == id && x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).ToList();
            return View();
        }

        //İzinOnaylama
        [HttpGet]
        public IActionResult ApprovedPermission(int id)
        {
            var permission = _permissionRepository.Permissions.Where(x => x.ID == id).FirstOrDefault();
            _permissionRepository.ApprovePermission(permission);
            return RedirectToAction("ListActivePermissions");
        }
        //İzinReddetme
        [HttpGet]
        public IActionResult DisApprovePermission(int id)
        {
            var permission = _permissionRepository.Permissions.FirstOrDefault(x => x.ID == id);
            _permissionRepository.DisApprovePermission(permission);

            return RedirectToAction("ListActivePermissions");
        }

        // YaklaşanDoğumGünleri ve İzin Taleplerini Görüntüleme
        [HttpGet]
        public IActionResult ShowUpComingEvent(Personnel personnel, Permission permission)
        {

            var personnels = _personnelRepository.Personnels.Where(x => x.BirthDate.Month == DateTime.Now.Month && (x.BirthDate.Day - DateTime.Now.Day) <= 3).ToList();
            var permissions = _permissionRepository.Permissions.Where(x => x.StartedDate.Month == DateTime.Now.Month && x.StartedDate.Day - DateTime.Now.Day <= 3).Include(x => x.Personnel).ToList();


            return View(Tuple.Create<IEnumerable<Personnel>, IEnumerable<Permission>>(personnels, permissions));


        }

        [HttpPost]
        public IActionResult ShowUpComingEvent([Bind(Prefix = "item1")] IEnumerable<Personnel> item1, [Bind(Prefix = "item2")] IEnumerable<Permission> item2)
        {
            return View();
        }

        //PersonelListeleme
        public IActionResult ListPersonels()
        {

            List<PersonelVm> personelVms = _personnelRepository.Personnels.Select(x => new PersonelVm
            {
                Name = x.Name,
                Surname = x.Surname,
                MailAddress = x.MailAddress,
                Address = x.Address,
                Telephone = x.Telephone,
                BirthDate = x.BirthDate,
                City = x.City,
                Department = x.Department,
                HiredDate = x.HiredDate,

            }).ToList();
            return View(personelVms);
        }

        //AktiveHarcamaListeleme
        public IActionResult ListActiveSpending()
        {
            ViewData[("AktiveSpendings")] = _spendingRepository.Spendings.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).ToList();


            ViewBag.BekleyenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();

            return View();
        }

        public IActionResult ListAproovedSpending()
        {
            ViewData[("PersonelSpendingApprovals")] = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).ToList();


            ViewBag.BekleyenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();

            return View("ListAproovedSpending");
        }

        public IActionResult ListDissaprovedSpending()
        {
            ViewData[("PersonelSpendingsDisapprovals")] = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).ToList();

            ViewBag.BekleyenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.ReddedilenSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == false).Include(x => x.Personnel).Count();
            ViewBag.OnaylananSayisi = _spendingRepository.Spendings.Where(x => x.IsActived == false && x.IsApproved == true).Include(x => x.Personnel).Count();

            return View("ListDissaprovedSpending");
        }


        //KişiBazlıHarcamaGörüntüleme
        [HttpGet]
        public IActionResult ListPersonelSpending(int id)
        {
            ViewData[("PersonelSpendingDetails")] = _spendingRepository.Spendings.Where(x => x.PersonnelID == id && x.IsActived == true && x.IsApproved == false).Include(x => x.Personnel).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult DisApproveSpending(int id)
        {
            var spending = _spendingRepository.Spendings.Where(x => x.ID == id).FirstOrDefault();
            _spendingRepository.DisApproveSpending(spending);

            return RedirectToAction("ListActiveSpending");
        }
        [HttpGet]
        public IActionResult ApproveSpending(int id)
        {
            var spending = _spendingRepository.Spendings.Where(x => x.ID == id).FirstOrDefault();
            _spendingRepository.ApproveSpending(spending);

            return RedirectToAction("ListActiveSpending");
        }

        //PersonelDetayı
        public IActionResult PersonnelDetails(int id)
        {
            return View(_personnelRepository.GetByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonnelDetails(Personnel personnel)
        {
            personnel.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            _personnelRepository.UpdatePersonnel(personnel);
            return RedirectToAction("ListPersonnel");
            //return View(_personnelRepository.Personnels.FirstOrDefault(pers => pers.ID == personnel.ID));
        }

        public IActionResult PersonnelDetail(int id)
        {
            return View(_personnelRepository.GetByID(id));
        }
        [HttpPost]
        public IActionResult PersonnelDetail()
        {
            List<Personnel> personelDetay = _personnelRepository.Personnels.Select(x => new Personnel
            {
                Name = x.Name,
                Surname = x.Surname,
                MailAddress = x.MailAddress,
                Address = x.Address,
                Telephone = x.Telephone,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                City = x.City,
                Department = x.Department,
                HiredDate = x.HiredDate,
                FiredDate = x.FiredDate

            }).ToList();
            return View(personelDetay);
        }


        [HttpGet]
        public IActionResult PersonalInfo(int id)
        {
            var perupdate = _personnelRepository.Personnels.SingleOrDefault(x => x.ID == id);

            return View(perupdate);
            //return View(_personnelRepository.GetByID(id));
        }

        [HttpPost]
        public IActionResult PersonalInfo(int id, Personnel personnel, bool Gender)
        {
            Personnel update = _personnelRepository.GetByID(id);
            update.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));



            if (personnel.FileNamePath != null)
            {
                string files = Path.Combine(_personnelEnvironment.WebRootPath, "files");
                if (personnel.FileNamePath.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(files, personnel.FileNamePath.FileName), FileMode.Create))
                    {
                        personnel.FileNamePath.CopyTo(fileStream);
                    }
                }
                update.FileName = personnel.FileNamePath.FileName;
            }
            update.BirthDate = personnel.BirthDate;
            update.HiredDate = personnel.HiredDate;
            update.IsActived = true;
            update.Gender = Gender;
            update.ModifiedDate = DateTime.Now;
            update.FiredDate = personnel.FiredDate;
            update.Address = personnel.Address;
            update.City = personnel.City;
            update.Salary = personnel.Salary;
            update.FileNamePath = personnel.FileNamePath;

            update.IsDeleted = false;

            _personnelRepository.UpdatePersonnel(update);
            return RedirectToAction("ListPersonnel");
        }

        //YorumGörüntüleme
        [HttpGet]
        public IActionResult ManagerPanel()
        {

            //ViewBag.Manager = _managerRepository.Managers.ToList();//ilerde bunu kaldırıp Tupple kullanabilirsin

            return View(Tuple.Create<IEnumerable<ManagerComment>, IEnumerable<Manager>>(_managerCommentRepository.Comments.ToList(), _managerRepository.Managers.ToList()));
        }
        [HttpPost]
        public IActionResult ManagerPanel([Bind(Prefix = "item1")] ManagerComment item1, [Bind(Prefix = "item2")] Manager item2)
        {
            return View();
        }


        [HttpGet]
        public IActionResult InsertManagerComment() => View();




        [HttpPost]
        public IActionResult InsertManagerComment(ManagerComment managerComment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    managerComment.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                    _managerCommentRepository.AddComment(managerComment);

                }
                catch (Exception)
                {
                    ViewBag.Exception = "Birden fazla yorum giremezsiniz";

                }

                return View(managerComment);

            }
            else
            {
                return View(managerComment);
            }

        }
        //ManagerBilgisiGüncelle
        public IActionResult UpdateManagerComment() => View(_managerRepository.GetById(Convert.ToInt32(HttpContext.Session.GetInt32("id"))));
        [HttpPost]
        public IActionResult UpdateManagerComment(ManagerComment managerComment)
        {

            managerComment.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            _managerCommentRepository.UpdateComment(managerComment);
            return RedirectToAction("ManagerPanel");
        }
        [HttpGet]
        public IActionResult DeleteManagerComment(int id)
        {
            ManagerComment managerComment = _managerCommentRepository.Comments.FirstOrDefault(x => x.ID == id);

            managerComment.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

            if (managerComment == null)
            {
                return NotFound();
            }

            return View(managerComment);

        }
        [HttpPost, ActionName("DeleteManagerComment")]
        public IActionResult DeleteManagerCommentConfirmed(int id)
        {
            _managerCommentRepository.DeleteComment(id);
            return RedirectToAction("ManagerPanel", "Manager");
        }
        //[HttpGet]
        //public IActionResult AddPermission()
        //{
        //    ViewBag.permissionsList = _permissionRepository.Permissions.Where(x => x.Name != null).ToList();
        //    return View();
        //}



        public IActionResult ShowCalendar()
        {
            return View();
        }

        public IActionResult GetCalendarEvents()
        {
            var events = _context.Calenders.ToList();
            return new JsonResult(events);
        }


    }
}
