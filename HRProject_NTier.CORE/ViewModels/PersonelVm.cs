using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    [NotMapped]
    public class PersonelVm : BaseEntity
    {

        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Display(Name = "Adresi")]
        public string Address { get; set; }

        [Display(Name = "Telefon No")]
        public string Telephone { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }


        [Display(Name = "Şehir")]
        public string City { get; set; }


        [Display(Name = "Departmanı")]
        public string Department { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? HiredDate { get; set; }

        [Display(Name = "Profil Fotoğrafı")]
        public string ProfileImage { get; set; }

    }
}
