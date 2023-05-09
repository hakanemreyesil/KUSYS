using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstracts
{
    //içini doldurmak için interfaceleri burada oluşturdum
    public interface IRepository
    {
        IQueryable<T> GetIQueryableObject<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetIQueryableObject<T>() where T : class;

        T GetEntityObject<T>(int Id) where T : class, new();
        T GetEntityObject<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class;

        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void Modify<T>(T entity) where T : class;
        int SaveChanges();
    }
}
