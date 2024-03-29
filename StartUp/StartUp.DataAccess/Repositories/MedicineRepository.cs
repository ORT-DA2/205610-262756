﻿using StartUp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>
    {
        private StartUpContext _context;
        public MedicineRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }
        public override IEnumerable<Medicine> GetAllByExpression(Expression<Func<Medicine, bool>> expression)
        {
            List<Medicine> medicines = new List<Medicine>(_context.Medicines.Where(expression));

            if (medicines != null)
            {
                return medicines.ToList();
            }

            return null;
        }

        public override Medicine GetOneByExpression(Expression<Func<Medicine, bool>> expression)
        {
            return _context.Set<Medicine>().Include("Symptoms").FirstOrDefault(expression);
        }

        public override void InsertOne(Medicine elem)
        {
            _context.Set<Medicine>().Add(elem);
        }
    }
}
