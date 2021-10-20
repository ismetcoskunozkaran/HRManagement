using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Abstract
{
   public interface IPaymentRepository
    {
        IQueryable<Payment> Payments { get; }
        bool AddPayment(Payment payment);
        bool AddPayment(InsertPaymentVM paymentVM);
        Payment GetById(int id);
        //bool UpdatePayment(Payment payment, int id);
        bool UpdatePayment(Payment payment);
        bool UpdatePayment(InsertPaymentVM paymentVM);
        bool DeletePayment(Payment payment);
    }
}
