using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
   public interface ICompanyRepository
    {

        IQueryable<Company> Companies { get; }

        bool InsertCompany(Company company);

        Company GetByID(int id);

        bool UpdateCompany(Company company);

        bool DeleteCompany(int id);
    }
}
