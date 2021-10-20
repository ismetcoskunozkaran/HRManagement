using HRProject_NTier.CORE.Entities;
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
    public class LoginController : Controller
    {
        private IPersonnelRepository _personnelRepository;
        public LoginController(IPersonnelRepository personnelRepository)
        {
            _personnelRepository = personnelRepository;
        }

        public IActionResult LoginForm()
        {
            ViewBag.Personnels = _personnelRepository.Personnels.ToList();
            return View();
        }


        [Route("PersonnelArea/Login")]
        [HttpPost]
        public IActionResult PersonnelLogin(string username, string password)
        {
            Personnel giris = _personnelRepository.Personnels.FirstOrDefault(x => x.Name == username && x.Password == password);
            if (giris != null && username.Equals(giris.Name) && password.Equals(giris.Password))
            {
                //HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetInt32("id", giris.ID);//ManagerID





                return RedirectToAction("Index", "Home", new { area = "PersonnelArea" });
                // return RedirectToAction("Index", "Home", new { area = "ManagerArea" });




            }
            else
            {
                ViewBag.invalidManagerAccount = "Kullanıcı adı veya şifre hatalı";



                //return View("LoginForm");
                return RedirectToAction("LoginForm");
            }

        }
        [Route("PersonnelArea/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("LoginForm");
        }
    }
}
