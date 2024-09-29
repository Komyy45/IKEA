using Linkdev.IKEA.DAL.Entities;

namespace Linkdev.IKEA.DAL.Presistance.Repositories._Generic
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking);

		public IQueryable<T> GetIQueryable();

		public IEnumerable<T> GetIEnumerable();

		Task<T?> GetAsync(int id);

        void Add(T T);

        void Update(T T);

        void Delete(T T);
	}
}
