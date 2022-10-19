using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class InvitationRepository : BaseRepository<Invitation>
    {
        private StartUpContext _context;
        public InvitationRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }
        
        public override IEnumerable<Invitation> GetAllByExpression(Expression<Func<Invitation, bool>> expression)
        {
             return _context.Set<Invitation>().Where(expression).Include("Pharmacy").ToList();
        }

        public override Invitation GetOneByExpression(Expression<Func<Invitation, bool>> expression)
        {
            return _context.Set<Invitation>().Include("Pharmacy").FirstOrDefault(expression);
        }

        public override void InsertOne(Invitation elem)
        {
            _context.Invitations.Add(elem);
        }
    }
}
