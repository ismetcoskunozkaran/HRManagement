using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public class Admin : BaseEntity
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

        [MaxLength(100, ErrorMessage = "en fazla 100 karakter girilebilir")]
        public string CompanyName { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefon formatında bilgi girmeniz gerekiyor.")]
        [StringLength(25, MinimumLength = 9, ErrorMessage = "Telefon numarası 9 ile 25 karakter olması gerekir")]
        public string Telephone { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<VisitorComments> VisitorComments { get; set; }

    }
}
