using System;

namespace Trimfit.DataAccess
{
    public interface ICommonRepository<TEntity>
    {
        public TEntity GetById(int id);
    }

    public class CommonRepository<TEntity> : ICommonRepository<TEntity>
    {
        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
