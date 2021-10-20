using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class PackageRepository : IPackageRepository
    {
        private ProjectContext _context;
        public PackageRepository(ProjectContext context)
        {
            _context = context;
        }
        public IQueryable<Package> Packages => _context.Packages;

        public bool InsertPackage(Package package)
        {
            _context.Packages.Add(package);
            return _context.SaveChanges() > 0;
        }
        public Package GetByID(int id)
        {
            return _context.Packages.Find(id);
        }
        public bool UpdatePackage(Package package)
        {
            _context.Packages.Update(package);
            return _context.SaveChanges() > 0;
        }
        public bool DeletePackage(int id)
        {
            _context.Packages.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }
    }
}
