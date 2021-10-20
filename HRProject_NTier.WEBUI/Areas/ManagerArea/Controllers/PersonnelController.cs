using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{
    [Area("ManagerArea")]
    public class PersonnelController : Controller
    {
        private readonly IPersonnelRepository _personnelrepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private ProjectContext _context;

        public PersonnelController(IPersonnelRepository personnelRepository, IWebHostEnvironment environment, IPermissionRepository permissionRepository, IPaymentRepository paymentRepository, ProjectContext projectContext)
        {
            _personnelrepository = personnelRepository;
            _permissionRepository = permissionRepository;
            _environment = environment;
            _paymentRepository = paymentRepository;
            _context = projectContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Payment
        //Avans Ekleme
        public IActionResult AddPayment()
        {
            var personelAdiSoyadi = _personnelrepository.GetByID(1);
            ViewData["personelAdi"] = personelAdiSoyadi.Name;
            ViewData["personelSoyadi"] = personelAdiSoyadi.Surname;
            ViewData["personelID"] = personelAdiSoyadi.ID;
            return View();
        }
        [HttpPost]
        public IActionResult AddPayment(InsertPaymentVM paymentVM)
        {
            _paymentRepository.AddPayment(paymentVM);
            ViewData["MesajKaydet"] = "Avans başarı ile kayıt edilmiştir.";
            return RedirectToAction("ListPayment");
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
                              PersonelID = per.ID
                          }).ToList();
            ViewData["onaylanan"] = result.Count();
            return View(result);
        }
        public IActionResult ResimDosyaEkle()
        {
            return View();
        }

        //resimDosya
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResimDosyaEkle([Bind("ID,IsActived,IsDeleted,CreatedDate,ModifiedDate,Name,Description,PersonnelID,InvoiceImageFile")] Spending spending)
        {
            //spending.ID = 1;
            //spending.IsActived = true;
            //spending.IsDeleted = false;
            //spending.CreatedDate = DateTime.Now;
            //spending.ModifiedDate = DateTime.Now;
            //spending.Name = "Harcama";
            //spending.Description = "Yemek Harcaması";
            spending.PersonnelID = 1;

            if (ModelState.IsValid)
            {
                ViewData["mesaj"] = "ModelState.Isvalid İçinde";


                var filePath = Path.Combine(_environment.WebRootPath, "Harcamalar");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                using (var dosyaAkisi = new FileStream(Path.Combine(filePath, spending.InvoiceImageFile.FileName), FileMode.Create))
                {
                    await spending.InvoiceImageFile.CopyToAsync(dosyaAkisi);
                }
                spending.InvoiceImage = spending.InvoiceImageFile.FileName;
                //p.FileName=Guid.NewGuid().ToString()+ 
                //fileName = Guid.NewGuid().ToString() + fi.Extension;

                _context.Add(spending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ResimDosyaListe));
            }
            return View(spending);
        }
        public IActionResult ResimDosyaListe()
        {
            var result = _context.Spendings;
            return View(result);
        }

        public IActionResult hesabiPasifeAl(int id)
        {
            Personnel personel = _personnelrepository.GetByID(Convert.ToInt32(id));
            if (personel.IsActived != false)
            {
                personel.IsActived = false;
                _personnelrepository.UpdatePersonnel(personel);

            }

            ViewData["mesaj"] = "Personel  hesabınız pasif hale geçmiştir.";

            return View(nameof(PersonelUyelikAyarlari));
        }
        public IActionResult PersonelUyelikAyarlari()
        {
            return View();
        }
        public IActionResult PersonnelShow()
        {

            ViewData["personnels"] = _personnelrepository.Personnels.ToList();
            return View();
        }

        [Route("/İzin Talebinde Bulun")]
        [HttpGet]
        public IActionResult AddPermission()
        {
            ViewData["PermissionTypes"] = _permissionRepository.Permissions.ToList();
            //ViewBag.PermissionTypes = _permissionRepository.Permissions.ToList();
            return View();
        }


        [Route("/İzin Talebinde Bulun")]
        [HttpPost]
        public IActionResult AddPermission(Permission permission)
        {
                _personnelrepository.AddPermission(permission);
                return RedirectToAction("ListActivePermissions", "Manager");
        }


        [HttpGet]
        public IActionResult UpdatePersonnel(int id)
        {
            return View(_personnelrepository.GetByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePersonnel(Personnel personnel)
        {
            personnel.ManagerID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            _personnelrepository.UpdatePersonnel(personnel);
            //return RedirectToAction("ListPersonnel");
            return RedirectToAction("PersonnelShow", "Personnel", new { area = "ManagerArea" });

        }
    }
}
