using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity  
    {
        private SalesEntities _context;

        static object locker = new object();

        public GenericRepository(SalesEntities salesDbContext)
        {
            _context = salesDbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {

                //return await _context.Set<T>().ToListAsync();

                var banner = _context.Set<T>().ToListAsync();
                Task.WaitAll();
                await Task.WhenAll(banner);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;

        }

        public T Find(Guid Id)
        {
            lock (locker)
            {
                return _context.Set<T>().Find(Id);
            }
        }

        public async Task<T> FindAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public void Add(T entity)
        {

            var banner = new Task(() => _context.Set<T>().Add(entity));
            Task.WaitAll();
            Task.WhenAll(banner);
           
            SaveChanges();
        }

        public void Add(IEnumerable<T> entity)
        {
            //_context.Set<T>().AddRange(entity);

            var banner = new Task(() => _context.Set<T>().AddRange(entity));
            Task.WaitAll();
            Task.WhenAll(banner);

            SaveChanges();
        }

        //public async Task AddRangeAsync(IEnumerable<T> entities)
        //{
        //    //var entityEntries = new List<EntityEntry>();
        //    foreach (var item in entities)
        //    {
        //        var addedEntity = await _context[""].AddAsync(item);
        //        entityEntries.Add(addedEntity);
        //    }

        //    await _weatherDbContext.SaveChangesAsync();

        //    foreach (var item in entityEntries)
        //    {
        //        item.State = EntityState.Detached;
        //    }
        //}

        public void Delete(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }

        public void SaveChanges()
        {
            var banner = new Task(() => _context.SaveChanges());
            Task.WaitAll();
            Task.WhenAll(banner);

            //_context.SaveChanges();
        }
    }
}
