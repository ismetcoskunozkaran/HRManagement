using System;
using System.Collections.Generic;
using System.Text;

namespace HRProject_NTier.CORE.Entities
{
    public class Topic
    {
        public int ID { get; set; }

        public string Name { get; set; }
        //Relations
        public virtual ICollection<CalenderTopic> CalenderTopics { get; set; }
    }
}
