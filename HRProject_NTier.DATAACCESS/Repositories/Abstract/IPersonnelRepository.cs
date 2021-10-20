using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
    public interface IPersonnelRepository
    {
        IQueryable<Personnel> Personnels { get; }

        bool InsertPersonnel(Personnel personnel);
        //bool InsertPersonnel(InsertPersonnelVM personnelVM);

        bool InsertPersonelPhoto(InsertPersonnelVM personnelVM);

        bool AddPermission(Permission permission);
       
        Personnel GetByID(int id);

      
        bool UpdatePersonnel(Personnel personnel);

        bool DeletePersonnel(int id);
    }
}
