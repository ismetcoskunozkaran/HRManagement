using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
    public interface IManagerRepository
    {
        IQueryable<Manager> Managers { get; }
        bool AddManager(Manager manager);
        Manager GetById(int id);
        bool UpdateManager(Manager manager);
        bool DeleteManager(int id);
        //string AddManagerComment(Manager manager);
    }
}
