using EasyShop.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyShop.Repository.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext context;

        public EfGenericRepository(DbContext context) => this.context = context;

        public void Add(T entity) => context.Set<T>().Add(entity);

        public void Delete(T entity) => context.Set<T>().Remove(entity);

        public void Edit(T entity) 
            => context.Entry(entity).State = EntityState.Modified;

        public IQueryable<T> Find(Expression<Func<T, bool>> expression) 
            => context.Set<T>().Where(expression);

        public T Get(int id) => context.Set<T>().Find(id);

        public IQueryable<T> GetAll() => context.Set<T>();

        public void Save() => context.SaveChanges();
    }
}
