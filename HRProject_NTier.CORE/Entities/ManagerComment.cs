using System;
using System.Collections.Generic;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public class ManagerComment
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int ManagerID { get; set; }//FK
        public Manager Manager { get; set; }
    }
}
