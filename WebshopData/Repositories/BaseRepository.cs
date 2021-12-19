using Microsoft.EntityFrameworkCore;
using WebshopShared.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebshopData.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly WebshopDbContext _WebshopDbContext;
        public BaseRepository(WebshopDbContext myDbContext)
        {
            _WebshopDbContext = myDbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _WebshopDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _WebshopDbContext.Set<T>().ToListAsync();
        }



        public async Task<T> AddAsync(T entity)
        {
            await _WebshopDbContext.Set<T>().AddAsync(entity);
            await _WebshopDbContext.SaveChangesAsync();

            return entity;
        }


        public async Task DeleteAsync(T entity)
        {
            _WebshopDbContext.Set<T>().Remove(entity);
            await _WebshopDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _WebshopDbContext.Update(entity);
            await _WebshopDbContext.SaveChangesAsync();

        }
    }
}
