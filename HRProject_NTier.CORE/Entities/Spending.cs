using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public enum SpendingType
    {
        [Display(Name = "Taksi Harcaması")]
        taxiSpending = 10,

        [Display(Name = "Yemek Harcaması")]
        mealSpending = 20,


    }
    public class Spending : BaseEntity
    {

        public string Description { get; set; }


        public string InvoiceImage { get; set; }

        [NotMapped]
        public IFormFile InvoiceImageFile { get; set; }

        public bool IsApproved { get; set; }

        public SpendingType spendingType { get; set; }


        [ForeignKey("Manager")]
        public int ManagerID { get; set; }


        [ForeignKey("Personnel")]
        public int PersonnelID { get; set; }

        //Relations
        public virtual Personnel Personnel { get; set; }
    }
}
