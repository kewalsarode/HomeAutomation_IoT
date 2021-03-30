using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext DbContext { get; set; }

        public Repository(EF.DeviceManagementEntities dataContext)
        {
            DbSet = dataContext.Set<T>();
            DbContext = dataContext;
        }

        public T Create()
        {
            return DbSet.Create();
        }

        public T Delete(T entity)
        {
            return DbSet.Remove(entity);
        }

        public int ExcuteStoreProc(string spNameWithParams, params object[] parameters)
        {
            string query = "EXEC " + spNameWithParams;
            return DbContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public T Insert(T entity)
        {
            var objEntity = DbSet.Add(entity);
            DbContext.SaveChanges();
            return objEntity;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
