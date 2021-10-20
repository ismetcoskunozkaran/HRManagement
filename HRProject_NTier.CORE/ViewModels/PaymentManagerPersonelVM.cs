using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRProject_NTier.CORE.ViewModels
{
    public class PaymentManagerPersonelVM : BaseEntity
    {
        public string PersonelName { get; set; }
        public string PersonelSurname { get; set; }
        public float PaymentAmount { get; set; }
        public string Description { get; set; }
        // public string Message { get; set; }
        public int PersonelID { get; set; }
        public bool IsApproved { get; set; }
    }
}
