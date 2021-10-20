using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Areas.ManagerArea.Controllers
{
    //[Route("account")]
    [Area("ManagerArea")]
    
    public class LoginController : Controller
    {
        private IManagerRepository _managerRepository;
        public LoginController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }
        //[Authorize]
        public IActionResult LoginForm()
        {
            return View();
        }


        [Route("ManagerArea/Login")]
        [HttpPost]
        public IActionResult ManagerLogin(string username, string password)
        {
            Manager giris = _managerRepository.Managers.FirstOrDefault(x => x.Name == username && x.Password == password);
            if (giris != null && username.Equals(giris.Name) && password.Equals(giris.Password))
            {
                //HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetInt32("id", giris.ID);//ManagerID





                return RedirectToAction("Index", "Home", new { area = "ManagerArea" });




            }
            else
            {
                ViewBag.invalidManagerAccount = "Kullanıcı adı veya şifre hatalı";



                //return View("LoginForm");
                return RedirectToAction("LoginForm");
            }
        }
        [Route("ManagerArea/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("LoginForm");
        }
    }
}
