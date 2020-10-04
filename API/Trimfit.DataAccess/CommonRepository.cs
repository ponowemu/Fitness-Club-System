using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trimfit.Domain;
using Trimfit.Domain.Abstractions;
using Trimfit.Logic;
using Trimfit.Utils.Extensions;

namespace Trimfit.DataAccess
{
    public interface ICommonRepository<TEntity>
    where TEntity : IDatabaseEntity
    {
        public IEnumerable<TEntity> GetAll();

        public TEntity GetById(int id);

        public bool Update(TEntity entity);

        public bool Remove(TEntity entity);
    }

    public class CommonRepository<TEntity> : ICommonRepository<TEntity> 
        where TEntity : IDatabaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly ILogger _logger;

        public CommonRepository(
            DatabaseContext context,
            ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.SetQuery<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _context.SetQuery<TEntity>()
                .FirstOrDefault(x => x.Id == id);
        }

        public bool Update(TEntity entity)
        {
            var dbEntity = GetById(entity.Id);

            if (dbEntity == null)
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.Log(LogLevel.Error, TextLogs.Concurrency_in_database_occured);

                return false;
            }
        }

        public bool Remove(TEntity entity)
        {
            var dbEntity = GetById(entity.Id);

            if (dbEntity == null)
            {
                return false;
            }

            _context.Entry(dbEntity).State = EntityState.Deleted;

            _context.SaveChanges();

            return false;
        }
    }
}
