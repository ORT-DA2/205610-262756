using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace StartUp.IDataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllExpression(Expression<Func<T, bool>> expression);
        T GetOneByExpression(Expression<Func<T, bool>> expression);
        void InsertOne(T elem);
        void DeleteOne(T elem);
        void UpdateOne(T elem);
        void Save();
        bool ToList();
    }
}

