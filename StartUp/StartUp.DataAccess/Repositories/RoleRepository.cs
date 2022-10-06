using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private StartUpContext _context;
        public RoleRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Role GetOneByExpression(Expression<Func<Role, bool>> expression)
        {
            return _context.Set<Role>().FirstOrDefault(expression);
        }

        public override void InsertOne(Role elem)
        {
            _context.Set<Role>().Add(elem);
        }
    }
}
