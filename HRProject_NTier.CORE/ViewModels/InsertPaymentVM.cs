using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    public class InsertPaymentVM
    {
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Ücret")]
        public float Amount { get; set; }

        public int PersonelID { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool IsActived { get; set; }
    }
}
