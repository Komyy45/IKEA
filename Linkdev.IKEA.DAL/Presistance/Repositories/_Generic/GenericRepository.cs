using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.IKEA.DAL.Entities;
using Linkdev.IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Linkdev.IKEA.DAL.Presistance.Repositories._Generic
{
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext) 
		{
			_dbContext = dbContext;
		} 

		public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking)
		{
			if (withAsNoTracking)
				return await _dbContext.Set<T>().Where(T => !T.IsDeleted).AsNoTracking().ToListAsync();

			return await _dbContext.Set<T>().Where(T => !T.IsDeleted).ToListAsync();
		}

		public IQueryable<T> GetIQueryable()
		{
			return _dbContext.Set<T>().Where(X => !X.IsDeleted);
		}

        public IEnumerable<T> GetIEnumerable()
        {
			return _dbContext.Set<T>();
        }

		public async Task<T?> GetAsync(int id)
		{
			return  await _dbContext.Set<T>().FindAsync(id);
		}

		public void Add(T T)
		{
			_dbContext.Set<T>().Add(T);
		}

		public void Update(T T)
		{
			_dbContext.Set<T>().Update(T);
		}

		public void Delete(T T)
		{
			T.IsDeleted = true;
            Update(T);
		}
    }
}
