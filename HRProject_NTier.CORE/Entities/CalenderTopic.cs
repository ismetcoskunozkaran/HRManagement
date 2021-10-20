using System;
using System.Collections.Generic;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
   public class CalenderTopic
    {
        public int CalenderID { get; set; }
        public int TopicID { get; set; }

        //Relations
        public Calender Calender { get; set; }
        public Topic Topic { get; set; }
    }
}
