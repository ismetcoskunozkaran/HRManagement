using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
   public interface IVisitorCommentsRepository
    {
        IQueryable<VisitorComments> VisitorComments { get; }

        bool InsertVisitorComments(VisitorComments visitorComments);

        VisitorComments GetByID(int id);

        bool UpdateVisitorComments(VisitorComments visitorComments);

        bool DeleteVisitorComments(int id);
    }
}
