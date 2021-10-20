using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
   public interface IPackageRepository
    {
        IQueryable<Package> Packages { get; }
        bool InsertPackage(Package package);
        Package GetByID(int id);
        bool UpdatePackage(Package package);
        bool DeletePackage(int id);
    }
}
