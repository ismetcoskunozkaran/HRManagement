using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public class Personnel : BaseEntity
    {
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Display(Name = "Şifresi")]
        public string Password { get; set; }

        [Display(Name = "Adresi")]
        public string Address { get; set; }

        [Display(Name = "Telefon No")]
        public string Telephone { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Profil Fotoğrafı")]
        public string ProfileImage { get; set; }

        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }

        [Display(Name = "Cinsiyeti")]
        public bool? Gender { get; set; }

        [Display(Name = "Şehir")]
        public string City { get; set; }

        [Display(Name = "Şirket Adı")]
        public string CompanyName { get; set; }

        [Display(Name = "Departmanı")]
        public string Department { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? HiredDate { get; set; }

        [Display(Name = "İşten Çıkış Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? FiredDate { get; set; }

        [Display(Name = "Maaş")]
        public float? Salary { get; set; }
        public string PersonnelMailBody { get; set; }//personele maili cke editör ile site içerisinden göndermek için gereklidir.


        [ForeignKey("Manager")]
        public int ManagerID { get; set; }
        //Relations
        public Manager Manager { get; set; }


        public string FileName { get; set; }

        [NotMapped]
        public IFormFile FileNamePath { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Spending> Spendings { get; set; }
    }
}
