using System;
using System.Linq;
using System.Linq.Expressions;

namespace EasyShop.Repository.Abstract
{
    public interface IGenericRepository<T> where T:class
    {
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}