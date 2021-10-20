using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public enum PermissionType
    {
        [Display(Name = "Doğum İzni")]
        parentalLeave = 10,

        [Display(Name = "İş Arama İzni")]
        seekingForJobLeave = 20,

        [Display(Name = "Evlilik İzni")]
        marriageLeave = 30,

        [Display(Name = "Süt İzni")]
        breastFeedingLeave = 40,

        [Display(Name = "Ücretsiz İzin")]
        unpaidLeave = 50,
    }
    public class Permission : BaseEntity
    {

        [Display(Name = "İzin Açıklaması")]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "İzin Açıklaması boş geçilemez")]
        public string Description { get; set; }

        [Display(Name = "İzin Başlangıç Tarihi")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "İzin Başlangıç tarihi boş geçilemez")]
        public DateTime StartedDate { get; set; }

        [Display(Name = "İzin Başlangıç Bitiş")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "İzin bitiş tarihi boş geçilemez")]
        public DateTime EndDate { get; set; }
        public bool IsApproved { get; set; }
        public PermissionType permissionType { get; set; }


        [ForeignKey("Manager")]
        public int ManagerID { get; set; }

        [ForeignKey("Personnel")]
        public int PersonnelID { get; set; }


        public virtual Personnel Personnel { get; set; }
    }
}
