using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HRProject_NTier.CORE.Entities
{
   public class Company : BaseEntity
    {

        [StringLength(500, ErrorMessage = "En Fazla {1} karakter uzunluğunda olmalıdır. ")]
        [Display(Name = "Aciklama")]

        public string Description { get; set; }

        [Display(Name = "LogoResmi")]
        public string LogoImage { get; set; }
        [NotMapped]
        public IFormFile LogoImageFile { get; set; }

        [StringLength(100, ErrorMessage = "En fazla {1} karakter uzunluğunda olmalıdır.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [ForeignKey("Package")]
        public int PackageID { get; set; }


        //[ForeignKey("Manager")]


        public int ManagerID { get; set; }

        //Relations
        public Manager Manager { get; set; }
        public Package Package { get; set; }
    }
}
