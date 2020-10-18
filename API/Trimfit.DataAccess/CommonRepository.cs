using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<TEntity> GetById(int id);

        public Task<bool> Create(TEntity entity);

        public Task<bool> Update(TEntity entity);

        public Task<bool> Remove(TEntity entity);
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

        public async Task<TEntity> GetById(int id)
        {
            return await _context.SetQuery<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Create(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.Log(LogLevel.Error, TextLogs.Concurrency_in_database_occured);

                return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            var dbEntity = await GetById(entity.Id);

            if (dbEntity == null)
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.Log(LogLevel.Error, TextLogs.Concurrency_in_database_occured);

                return false;
            }
        }

        public async Task<bool> Remove(TEntity entity)
        {
            var dbEntity = await GetById(entity.Id);

            if (dbEntity == null)
            {
                return false;
            }

            _context.Entry(dbEntity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return false;
        }
    }
}
