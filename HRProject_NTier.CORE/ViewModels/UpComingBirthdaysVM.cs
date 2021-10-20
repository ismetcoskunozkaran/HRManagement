using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    public class UpComingBirthdaysVM : BaseEntity
    {
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        public int UpComingBirthday { get; set; }



        public string Department { get; set; }
    }
}
