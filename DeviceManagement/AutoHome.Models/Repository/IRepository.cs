using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.Models.Repository
{
    public interface IRepository<T>
    {
        T Insert(T entity);
        void Update(T entity);
        T Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        T Create();
        void Commit();
        /// <summary>
        /// Executes a store procedure that contains DDL/DML statements.
        /// </summary>
        /// <param name="spNameWithParams">Pass name of the store procedure with its parameter names. e.g. sp_testProc @testId.</param>
        /// <param name="parameters">Pass the SQL paramenters with its value.</param>
        /// <returns>Number of rows affected.</returns>
        int ExcuteStoreProc(string spNameWithParams, params object[] parameters);
    }
}
