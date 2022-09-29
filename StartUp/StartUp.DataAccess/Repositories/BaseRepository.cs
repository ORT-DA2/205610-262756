using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess;
using StartUp.IDataAccess;

namespace StartUp.DataAccess;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly StartUpContext _context;

    public BaseRepository(StartUpContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAllExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public T GetOneByExpression(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().FirstOrDefault(expression);
    }

    public void InsertOne(T elem)
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