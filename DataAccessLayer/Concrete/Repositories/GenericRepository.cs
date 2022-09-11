using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context context = new Context();

        DbSet<T> _object;

        public GenericRepository()
        {
            _object = context.Set<T>();
        }
        public void Delete(T item)
        {
            var deletedEntity = context.Entry(item);
            deletedEntity.State = EntityState.Deleted;
           // _object.Remove(item);
            context.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
              return  _object.SingleOrDefault(filter);
        }

        public void Insert(T item)
        {
            var addedEntity = context.Entry(item);
            addedEntity.State = EntityState.Added;
          //  _object.Add(item);
            context.SaveChanges();
        }

        public List<T> List()
        {
            return _object.ToList();
          
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T item)
        {
            var updatedEntity = context.Entry(item);
            updatedEntity.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
