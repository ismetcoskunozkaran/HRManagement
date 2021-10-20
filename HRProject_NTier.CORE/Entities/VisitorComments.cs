using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
   public class VisitorComments
    {
        public int CommentsID { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        [ForeignKey("Admin")]
        public int AdminID { get; set; }

        public Admin Admin { get; set; }
    }
}
