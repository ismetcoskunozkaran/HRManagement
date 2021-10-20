using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class PermissionRepository : IPermissionRepository
    {
        private ProjectContext _context;

        public PermissionRepository(ProjectContext context)
        {
            _context = context;

        }

        public IQueryable<Permission> Permissions => _context.Permissions;

        public bool DeletePermission(int id)
        {
            _context.Permissions.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }

        public Permission GetByID(int id)
        {
            return _context.Permissions.Find(id);
        }

        public bool InsertPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePermission(Permission permission)
        {
            _context.Permissions.Update(permission);
            return _context.SaveChanges() > 0;
        }

        public bool ApprovePermission(Permission permission)
        {

            permission.IsActived = false;
            permission.IsApproved = true;
            _context.Permissions.Update(permission);
            return _context.SaveChanges() > 0;
        }
        public bool DisApprovePermission(Permission permission)
        {
            permission.IsActived = false;
            permission.IsApproved = false;
            _context.Permissions.Update(permission);
            return _context.SaveChanges() > 0;
        }

    }
}
