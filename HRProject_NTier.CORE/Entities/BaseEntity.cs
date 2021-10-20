using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        public bool IsActived { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [MaxLength(50, ErrorMessage = "İsim 50 karakterden fazla olamaz")]
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Name { get; set; }
    }
}
