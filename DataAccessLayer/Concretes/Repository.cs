using DataAccessLayer.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concretes
{
    //Generic yapıda kurdum tanımladığımız interfacelerin içini doldurdum 
    public class Repository : IRepository
    {
       Context context = new Context();
        public void Add<T>(T entity) where T : class
        {
            context.Add<T>(entity);
        }

        public void AddRange<T>(List<T> entities) where T : class
        {
            context.AddRange(entities);
        }

        public T GetEntityObject<T>(int Id) where T : class, new()
        {
            return context.Set<T>().Find(Id);
        }

        public T GetEntityObject<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetIQueryableObject<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetIQueryableObject<T>() where T : class
        {
            return context.Set<T>();
        }

        public void Modify<T>(T entity) where T : class
        {
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            var deleted = context.Remove(entity);
            deleted.State = EntityState.Deleted;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
