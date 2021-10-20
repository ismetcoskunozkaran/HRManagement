using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    public class ListPermissionsVM : BaseEntity
    {
        public string PermissionName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "İzin Açıklaması")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "İzin Tipi")]
        public DateTime StartedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "İzin Bitiş Tarihi")]
        public DateTime EndDate { get; set; }
    }
}
