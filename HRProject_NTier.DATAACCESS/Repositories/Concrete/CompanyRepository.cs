using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class CompanyRepository : ICompanyRepository
    {

        private ProjectContext _context;

        public CompanyRepository(ProjectContext context)
        {
            _context = context;
        }
        public IQueryable<Company> Companies => _context.Companies;

        public bool InsertCompany(Company company)
        {
            _context.Companies.Add(company);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCompany(int id)
        {
            _context.Companies.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }

        public Company GetByID(int id)
        {
            //ID ye göre ürünü bulmak için find() metodunu kullanabiliriz.
            return _context.Companies.Find(id);
        }

        public bool UpdateCompany(Company company)
        {
            _context.Companies.Update(company);
            return _context.SaveChanges() > 0;
        }
    }
}
