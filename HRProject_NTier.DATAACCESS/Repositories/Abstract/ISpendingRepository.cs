using HRProject_NTier.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
    public interface ISpendingRepository
    {
        IQueryable<Spending> Spendings { get; }

        bool InsertSpending(Spending spending);

        Spending GetByID(int id);

        bool UpdateSpending(Spending spending);

        bool ApproveSpending(Spending spending);
        bool DisApproveSpending(Spending spending);

        bool DeleteSpending(int id);
    }
}
