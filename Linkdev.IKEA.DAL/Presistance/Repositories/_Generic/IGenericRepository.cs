using Linkdev.IKEA.DAL.Entities;

namespace Linkdev.IKEA.DAL.Presistance.Repositories._Generic
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		IEnumerable<T> GetAll(bool withAsNoTracking);

		public IQueryable<T> GetIQueryable();

		public IEnumerable<T> GetIEnumerable();

		T? Get(int id);

		int Add(T T);

		int Update(T T);

		int Delete(T T);
	}
}
