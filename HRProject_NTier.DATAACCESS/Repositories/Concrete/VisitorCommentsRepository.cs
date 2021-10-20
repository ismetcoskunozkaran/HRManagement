using HRProject_NTier.CORE.Entities;
using HRProject_NTier.DATAACCESS.Context;
using HRProject_NTier.DATAACCESS.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRProject_NTier.DATAACCESS.Repositories.Concrete
{

  
    public class VisitorCommentsRepository : IVisitorCommentsRepository
    {
        private ProjectContext _context;

        public VisitorCommentsRepository(ProjectContext context)
        {
            _context = context;
        }
        IQueryable<VisitorComments> IVisitorCommentsRepository.VisitorComments => _context.VisitorComments;

        public bool DeleteVisitorComments(int id)
        {
            _context.VisitorComments.Remove(GetByID(id));
            return _context.SaveChanges() > 0;
        }

        public VisitorComments GetByID(int id)
        {
            return _context.VisitorComments.Find(id);
        }

        public bool InsertVisitorComments(VisitorComments visitorComments)
        {
            _context.VisitorComments.Add(visitorComments);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateVisitorComments(VisitorComments visitorComments)
        {
            _context.VisitorComments.Update(visitorComments);
            return _context.SaveChanges() > 0;
        }
    }
}
