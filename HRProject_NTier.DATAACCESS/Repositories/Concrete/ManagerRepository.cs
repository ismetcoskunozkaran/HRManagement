using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class ManagerRepository : IManagerRepository
    {
        private ProjectContext _context;
        public ManagerRepository(ProjectContext context)
        {
            _context = context;
        }
        public IQueryable<Manager> Managers => _context.Managers;


        public Manager GetById(int id)
        {
            return _context.Managers.Find(id);
        }

        public bool AddManager(Manager manager)
        {
            _context.Managers.Add(manager);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteManager(int id)
        {
            _context.Managers.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }



        public bool UpdateManager(Manager manager)
        {
            _context.Managers.Update(manager);
            return _context.SaveChanges() > 0;
        }

    }
}
