using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
    public interface IPermissionRepository
    {
        IQueryable<Permission> Permissions { get; }

        bool InsertPermission(Permission permission);

        Permission GetByID(int id);

        bool UpdatePermission(Permission permission);

        bool ApprovePermission(Permission permission);
        bool DisApprovePermission(Permission permission);

        bool DeletePermission(int id);
    }
}
