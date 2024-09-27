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

		public IEnumerable<T> GetAll(bool withAsNoTracking)
		{
			if (withAsNoTracking)
				return _dbContext.Set<T>().Where(T => !T.IsDeleted).AsNoTracking().ToList();

			return _dbContext.Set<T>().Where(T => !T.IsDeleted).ToList();
		}

		public IQueryable<T> GetIQueryable()
		{
			return _dbContext.Set<T>().Where(X => !X.IsDeleted);
		}

        public IEnumerable<T> GetIEnumerable()
        {
			return _dbContext.Set<T>();
        }

		public T? Get(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public int Add(T T)
		{
			_dbContext.Set<T>().Add(T);
			return _dbContext.SaveChanges();
		}

		public int Update(T T)
		{
			_dbContext.Set<T>().Update(T);
			return _dbContext.SaveChanges();
		}

		public int Delete(T T)
		{
			T.IsDeleted = true;
            return Update(T);
		}
    }
}
