using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class SpendingRepository : ISpendingRepository
    {
        private ProjectContext _context;
        public SpendingRepository(ProjectContext context)
        {
            _context = context;
        }


        public IQueryable<Spending> Spendings => _context.Spendings;

        public bool ApproveSpending(Spending spending)
        {
            spending.IsActived = false;
            spending.IsApproved = true;
            _context.Spendings.Update(spending);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteSpending(int id)
        {
            _context.Spendings.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }

        public bool DisApproveSpending(Spending spending)
        {
            spending.IsActived = false;
            spending.IsApproved = false;
            _context.Spendings.Update(spending);
            return _context.SaveChanges() > 0;
        }

        public Spending GetByID(int id)
        {
            return _context.Spendings.Find(id);
        }

        public bool InsertSpending(Spending spending)
        {
            _context.Spendings.Add(spending);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateSpending(Spending spending)
        {
            _context.Spendings.Update(spending);
            return _context.SaveChanges() > 0;
        }
    }
}
