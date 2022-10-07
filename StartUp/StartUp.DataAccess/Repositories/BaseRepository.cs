using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using StartUp.IDataAccess;

namespace StartUp.DataAccess;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly StartUpContext _context;

    public BaseRepository(StartUpContext context)
    {
        _context = context;
    }

    public virtual IEnumerable<T> GetAllByExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual T GetOneByExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }

    public virtual void InsertOne(T elem)
    {
        _context.Set<T>().Add(elem);
    }

    public void DeleteOne(T elem)
    {
        _context.Set<T>().Remove(elem);
    }

    public void UpdateOne(T elem)
    {
        _context.Set<T>().Update(elem);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}