using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public enum PackageTime { threeMonths, sixMonths, oneYear }
    public class Package : BaseEntity
    {
        public int PersonnelNumber { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndDate { get; set; }

        public PackageTime PackageTime { get; set; }
        [Display(Name = "Fiyat")]
        public float Price { get; set; }
        //Relations
        public virtual ICollection<Company> Companies { get; set; }
    }
}
