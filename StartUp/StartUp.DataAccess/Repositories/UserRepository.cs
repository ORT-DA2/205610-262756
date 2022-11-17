using Microsoft.EntityFrameworkCore;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.Domain;

namespace StartUp.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private StartUpContext _context;

        public UserRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }
        
        public override IEnumerable<User> GetAllByExpression(Expression<Func<User, bool>> expression)
        {
            return _context.Set<User>().Where(expression).Include("Roles").ToList();
        }

        public override User GetOneByExpression(Expression<Func<User, bool>> expression)
        {
            return _context.Set<User>().Include("Invitation").Include("Roles").Include("Pharmacy").FirstOrDefault(expression);
        }

        public override void InsertOne(User elem)
        {
            _context.Set<User>().Add(elem);
        }

        public override void UpdateOne(User elem)
        {
            _context.Set<User>().Update(elem);
        }

    }
}
