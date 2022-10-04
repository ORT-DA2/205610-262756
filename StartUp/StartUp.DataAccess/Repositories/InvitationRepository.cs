using StartUp.Domain;
using System;
using System.Data.Entity;
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

        public override Invitation GetOneByExpression(Expression<Func<Invitation, bool>> expression)
        {
            return _context.Set<Invitation>().Include("Pharmacy").FirstOrDefault(expression);
        }

        public override void InsertOne(Invitation elem)
        {
            _context.Entry(elem).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Unchanged;
            _context.Set<Invitation>().Add(elem);

        }
    }
}
