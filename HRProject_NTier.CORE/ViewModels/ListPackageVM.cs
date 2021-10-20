using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    public class ListPackageVM : BaseEntity
    {
        [Display(Name = "Personel Sayısı")]
        public int PersonnelNumber { get; set; }

        [Display(Name = "Paket Başlangıç Tarihi")]
        public DateTime StartedDate { get; set; }

        [Display(Name = "Paket Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Paket Ücreti")]
        public float Price { get; set; }
    }
}
