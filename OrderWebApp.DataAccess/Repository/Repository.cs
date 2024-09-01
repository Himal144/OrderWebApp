using Microsoft.EntityFrameworkCore;
using OrderWebApp.DataAccess.Data;
using OrderWebApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext? _db;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet= _db.Set<T>();
            //_db.categories = _dbSet
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
         
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
         
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return query.ToList(); 
         
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
      
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
       
        }
    }
}
