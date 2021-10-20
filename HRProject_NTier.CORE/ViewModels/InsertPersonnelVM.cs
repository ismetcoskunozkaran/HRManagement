using HRProject_NTier.CORE.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    [NotMapped]
    public class InsertPersonnelVM : BaseEntity
    {
        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Display(Name = "MailAdresi")]
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }

        public string ProfileImage { get; set; }
        public string PersonnelMailBody { get; set; }
        public int ManagerID { get; set; }


        [Display(Name = "PersonelResmi")]
        public IFormFile FileName { get; set; }

        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }
    }
}
