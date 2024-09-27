using Linkdev.IKEA.DAL.Entities;

namespace Linkdev.IKEA.DAL.Presistance.Repositories._Generic
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		IEnumerable<T> GetAll(bool withAsNoTracking);

		public IQueryable<T> GetIQueryable();

		public IEnumerable<T> GetIEnumerable();

		T? Get(int id);

        void Add(T T);

        void Update(T T);

        void Delete(T T);
	}
}
