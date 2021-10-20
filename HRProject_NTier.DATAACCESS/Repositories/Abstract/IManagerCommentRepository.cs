using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
   public interface IManagerCommentRepository
    {
        IQueryable<ManagerComment> Comments { get; }
        bool AddComment(ManagerComment comment);
        ManagerComment GetById(int id);
        bool UpdateComment(ManagerComment comment);
        bool DeleteComment(int id);
    }
}
