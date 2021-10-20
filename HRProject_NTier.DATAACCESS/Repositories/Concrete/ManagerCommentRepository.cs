using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{
    public class ManagerCommentRepository : IManagerCommentRepository
    {
        private ProjectContext _context;
        public ManagerCommentRepository(ProjectContext context)
        {

            _context = context;
        }
        public IQueryable<ManagerComment> Comments => _context.ManagerComments;

        public ManagerComment GetById(int id)
        {
            return _context.ManagerComments.Find(id);

        }
        public bool AddComment(ManagerComment comment)
        {
            _context.ManagerComments.Add(comment);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteComment(int id)
        {
            _context.ManagerComments.Remove(GetById(id));
            return _context.SaveChanges() > 0;
        }



        public bool UpdateComment(ManagerComment comment)
        {
            _context.ManagerComments.Update(comment);
            return _context.SaveChanges() > 0;
        }
    }
}
