using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
   public class Manager : BaseEntity
    {
        [MaxLength(50, ErrorMessage = "Soyad 50 karakterden fazla olamaz")]
        [Required]
        public string Surname { get; set; }
        [MaxLength(250, ErrorMessage = "Mail adresi 250 karakterden fazla olamaz")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string MailAddress { get; set; }
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [MaxLength(10, ErrorMessage = "Girilen şifre en fazla 10 karakter olması gerekiyor.")]
        [MinLength(8, ErrorMessage = "Girilen şifre en az 8 karakter olması gerekiyor.")]
        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        public string Password { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefon formatında bilgi girmeniz gerekiyor.")]
        [StringLength(25, MinimumLength = 9, ErrorMessage = "Telefon numarası 9 ile 25 karakter olması gerekir")]
        public string Telephone { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string ProfileImage { get; set; }
        [NotMapped]
        public IFormFile ProfileImageFile { get; set; }
        public bool? Gender { get; set; }
        [MaxLength(50, ErrorMessage = "en fazla 50 karakter girilebilir")]
        public string City { get; set; }
        [MaxLength(100, ErrorMessage = "en fazla 100 karakter girilebilir")]
        public string CompanyName { get; set; }
        [MaxLength(100, ErrorMessage = "en fazla 50 karakter girilebilir")]
        public string Department { get; set; }
        [DataType(DataType.Date)]
        public DateTime? HiredDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? FiredDate { get; set; }
        public float? Salary { get; set; }

        //Relations
        public Company Company { get; set; }
        public ManagerComment ManagerComment { get; set; }


        //public virtual ICollection<Permission> Permissions { get; set; }


        //public virtual ICollection<Spending> Spendings { get; set; }
        // public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Personnel> Personnels { get; set; }
        public virtual ICollection<Calender> Calenders { get; set; }
    }
}
