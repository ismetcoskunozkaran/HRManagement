using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HRProject_NTier.WEBUI.Models
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private ProjectContext _context;
        private IWebHostEnvironment webHostEnviroment;
        private IManagerRepository _managerRepository;
        private IPermissionRepository _permissionRepository;


        public PersonnelRepository(ProjectContext context, IWebHostEnvironment hostEnviroment,IManagerRepository managerRepository, IPermissionRepository permissionRepository)
        {
            _context = context;
            webHostEnviroment = hostEnviroment;
            _managerRepository = managerRepository;
            _permissionRepository = permissionRepository;
        }

        public IQueryable<Personnel> Personnels => _context.Personnels;

        public bool DeletePersonnel(int id)
        {
            _context.Personnels.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }

        public Personnel GetByID(int id)
        {
            return _context.Personnels.Find(id);
        }

        public bool InsertPersonnel(Personnel personnel)
        {
            _context.Personnels.Add(personnel);
            return _context.SaveChanges() > 0;
        }

        //public bool InsertPersonnel(InsertPersonnelVM personnelVM)
        //{


        //    Personnel personnel = new Personnel()
        //    {
        //        Name = personnelVM.Name,
        //        Surname = personnelVM.Surname,
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        IsActived = true,
        //        IsDeleted = false,
        //        MailAddress = personnelVM.MailAddress,
        //        Password = Guid.NewGuid().ToString(),
        //        CompanyName = personnelVM.CompanyName,
        //        Department = personnelVM.Department,
        //        ManagerID = 1                

        //    };
        //    _context.Personnels.Add(personnel);
        //    return _context.SaveChanges() > 0;
        //}


        public bool UpdatePersonnel(Personnel personnel)
        {
            _context.Personnels.Update(personnel);
            return _context.SaveChanges() > 0;
        }


        Random rnd = new Random();
        //HttpContext HttpContext;
        public bool InsertPersonelPhoto(InsertPersonnelVM personnelVM)
        {
         
            //HttpContext httpContext = new HttpContext();

            //int managerID=HttpContext.
            // Manager giris =_context.Managers.FirstOrDefault(x => x.Name == username && x.Password == password);

            //HttpContext.SessionSetInt32("id", );
            string uniqueFileName = UploadFile(personnelVM);

            Personnel personnel = new Personnel()
            {
                Name = personnelVM.Name,
                Surname = personnelVM.Surname,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActived = true,
                IsDeleted = false,
                MailAddress = personnelVM.MailAddress,
                PersonnelMailBody = personnelVM.PersonnelMailBody,

                Password = rnd.Next(1, 9).ToString() + Convert.ToChar(rnd.Next(97, 123)) + Convert.ToChar(rnd.Next(65, 91)) + Convert.ToChar(rnd.Next(97, 123)) + Convert.ToChar(rnd.Next(65, 91)) + Convert.ToChar(rnd.Next(97, 123)) + Convert.ToChar(rnd.Next(65, 91)) + rnd.Next(1, 99).ToString(),

                CompanyName = personnelVM.CompanyName,
                Department = personnelVM.Department,
                ManagerID = 1,
               
               
                //ManagerID=personnelVM.ManagerID,

                //ManagerID =Convert.ToInt32(HttpContext.Session.GetInt32("id")),

                ProfileImage = uniqueFileName

            };
            SendActivationMail(personnel);
            _context.Personnels.Add(personnel);
            return _context.SaveChanges() > 0;
        }

      

        //private void SendActivationMailNormal(Personnel personnel)
        //{


        //    using (MailMessage mm = new MailMessage("mvcblogproje1@gmail.com", personnel.MailAddress))

        //    {
        //        mm.Subject = "Personel Kullanıcı Adı ve şifresi";
        //        string body = "Merhaba ";
        //        body += "<br /><br />Şirketimizde işe başladığınıza çok seviniyoruz.Ailemize hoşgeldiniz.";
        //        body += $"<br /><a>Kullanıcı Adınız:{personnel.Name}</a>";
        //        body += $"<br /><a>Şifreniz:{personnel.Password}</a>";

        //        body += "<br /><br />Teşekkürler";
        //        body += "<br /><br />İyi Çalışmalar";
        //        mm.Body = body;
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        smtp.EnableSsl = true;

        //        NetworkCredential NetworkCred = new NetworkCredential("mvcblogproje1@gmail.com", "mv,c@blog.proje1");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 587;
        //        smtp.Send(mm);
        //    }
        //}


        private void SendActivationMail(Personnel personnel)
        {


            using (MailMessage mm = new MailMessage("mvcblogproje1@gmail.com", personnel.MailAddress))

            {
                mm.Subject = "Personel Kullanıcı Adı ve şifresi";
                string body = "";

                body += personnel.PersonnelMailBody;
                body += $"<br /><a>Kullanıcı Adınız:{personnel.Name}</a>";
                body += $"<br /><a>Şifreniz:{personnel.Password}</a>";

                body += "<br /><br />Teşekkürler";
                body += "<br />İyi Çalışmalar";

                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;

                NetworkCredential NetworkCred = new NetworkCredential("mvcblogproje1@gmail.com", "mv,c@blog.proje1");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }




        private string UploadFile(InsertPersonnelVM ınsertPersonnelVM)
        {
            string uniqueFileName = null;

            if (ınsertPersonnelVM.ProfileImageFile.ContentType.Contains("image"))
            {
                string uploadsFolder = Path.Combine(webHostEnviroment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ınsertPersonnelVM.ProfileImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ınsertPersonnelVM.ProfileImageFile.CopyTo(fileStream);
                    return filePath.Substring(filePath.IndexOf("\\images\\"));
                }
            }
            else
            {
                return $"Doğru belge giriniz.";
            }

        }

        public bool AddPermission(Permission permission)
        {
                permission.Name = "İzin Talebi";
                permission.IsActived = true;
                permission.IsApproved = false;
                permission.IsDeleted = false;
                permission.PersonnelID = 1;
                permission.ManagerID = 1;
              
               _context.Permissions.Add(permission);
                return _context.SaveChanges() > 0;


            //Permission permission = new Permission()
            //{
            //    Name = permissionVM.Name,
            //    IsActived = true,
            //    IsApproved = false,
            //    StartedDate = permissionVM.StartedDate,
            //    EndDate = permissionVM.EndDate,
            //    permissionType = (CORE.Entities.PermissionType)permissionVM.permissionType,
            //    PersonnelID = 1

            //};

        }

     
    }
}
