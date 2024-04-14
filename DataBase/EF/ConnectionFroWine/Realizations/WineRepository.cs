using System.Linq.Expressions;
using Core.Actions.Abstractions.DataBaseConnector;
using Core.Models.Abstractions;
using DataBase.EF.ConnectionFroWine.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DataBase.EF.ConnectionFroWine.Realizations
{
    /// <summary>
    /// Контект для виноделия
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WineRepository<T> : IBaseGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Контекст
        /// </summary>
        private WineDbContext _context;

        /// <summary>
        /// Таблица
        /// </summary>
        private DbSet<T> table;

        public WineRepository()
        {
            _context = new WineDbContext();

            table = _context.Set<T>();
        }

        public WineRepository(WineDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Add(T item)
        {
            table.Add(item);
        }

        public void Delete(int id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Delete(T item)
        {
            T existing = table.Find(item.Id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public IEnumerable<T> GetWithInclude(Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = table.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T item)
        {
            table.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
