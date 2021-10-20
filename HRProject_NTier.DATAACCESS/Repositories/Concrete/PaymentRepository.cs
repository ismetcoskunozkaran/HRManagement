using HRProject_NTier.CORE.Entities;
using HRProject_NTier.CORE.ViewModels;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private ProjectContext _context;

        public PaymentRepository(ProjectContext context)
        {
            _context = context;
        }
        public IQueryable<Payment> Payments => _context.Payments;

        //public IQueryable<Payment> Payments => new List<Payment>
        //{
        //    new Payment()
        //    {
        //        ID=1, Name="Ali", Description="Ek Ödeme", Amount=500, IsActived=true,  ManagerID=1
        //    },
        //    new Payment()
        //    {
        //        ID=2, Name="Bilge", Description="Bayram Ödemesi", Amount=850, IsActived=true,  ManagerID=1
        //    },
        //    new Payment()
        //    {
        //        ID=3, Name="Can", Description="8 Saatlik Mesai Ödemesi", Amount=700, IsActived=true,  ManagerID=1, 
        //    },
        //}.AsQueryable();

        public bool AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            return _context.SaveChanges() > 0;
        }
        //public bool AddPayment(InsertPaymentVM paymentVM)
        //{
        //    var result = (from p in _context.Payments
        //                  join m in _context.Managers
        //                  on p.ManagerID equals m.ID
        //                  join pr in _context.Personnels
        //                  on m.ID equals pr.ManagerID
        //                  where p.Name == pr.Name
        //                  select new InsertPaymentVM
        //                  {
        //                      Name = paymentVM.Name,
        //                      Surname = paymentVM.Surname,
        //                      Amount = paymentVM.Amount,
        //                      Description = paymentVM.Description,
        //                      CreatedDate = DateTime.Now,
        //                      IsActived = true,
        //                      ManagerID = m.ID
        //                  }).ToList();

        //Payment payment = result.Cast<Payment>().ToList();

        //Payment payment = new Payment()
        //{
        //    Name=paymentVM.Name,
        //    Surname = paymentVM.Surname,
        //    Amount = paymentVM.Amount,
        //    Description = paymentVM.Description,
        //    CreatedDate = DateTime.Now,
        //    IsActived = true,
        //    ManagerID =3

        //};

        //_context.Payments.Add(payment);
        //    return _context.SaveChanges() > 0;
        //}

        public IEnumerable<Payment> GetAll()
        {
            return _context.Payments.Where(x => x.IsActived == true).ToList();
        }
        public Payment GetById(int id)
        {
            return _context.Payments.Find(id);
        }
        public bool DeletePayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
        public bool UpdatePayment(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool AddPayment(InsertPaymentVM paymentVM)
        {
            Payment payment = new Payment()
            {
                Amount = paymentVM.Amount,
                Description = paymentVM.Description,
                CreatedDate = DateTime.Now,
                IsActived = false,
                PersonnelID = 1
            };
            _context.Payments.Add(payment);
            return _context.SaveChanges() > 0;
        }

        public bool UpdatePayment(InsertPaymentVM paymentVM)
        {
            throw new NotImplementedException();
        }

        //public bool UpdatePayment(InsertPaymentVM paymentVM)
        //{
        //    Payment payment = new Payment()
        //    {
        //        Name = paymentVM.Name,
        //        Amount = paymentVM.Amount,
        //        Description = paymentVM.Description,
        //        CreatedDate = DateTime.Now,
        //        IsActived = true,
        //        ManagerID = 1
        //    };
        //    if (payment.Name != null)
        //    {
        //        _context.Payments.Update(payment);
        //    }
        //    return _context.SaveChanges() > 0;
        //}
    }
}
