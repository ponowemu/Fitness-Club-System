using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trimfit.DataAccess;
using Trimfit.Domain.Abstractions;

namespace Trimfit.QueriesHandling
{
    public interface ICommonQueryHandler<TEntity> where TEntity : IDatabaseEntity
    {
        Task<IActionResult> HandlePostAction(TEntity entity);
        Task<IActionResult> HandlePutAction(TEntity entity);
        Task<IActionResult> HandleGetAction(TEntity entity);
        IEnumerable<TEntity> HandleGetAction();
        Task<IActionResult> HandleDeleteAction(TEntity entity);
    }

    public class CommonQueryHandler<TEntity> : ControllerBase, ICommonQueryHandler<TEntity> where TEntity : IDatabaseEntity
    {
        private readonly ICommonRepository<TEntity> _commonRepository;

        public CommonQueryHandler(ICommonRepository<TEntity> commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public async Task<IActionResult> HandlePostAction(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _commonRepository.Create(entity))
            {
                return CreatedAtAction(typeof(TEntity).FullName, entity);
            }

            //TODO.MK: Handle concurrency or bad request
            return NoContent();
        }

        public async Task<IActionResult> HandlePutAction(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _commonRepository.Update(entity))
            {
                return CreatedAtAction(typeof(TEntity).FullName, entity);
            }

            //TODO.MK: Handle concurrency or bad request
            return NoContent();
        }

        public async Task<IActionResult> HandleGetAction(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbEntity = await _commonRepository.GetById(entity.Id);

            if (dbEntity == null)
            {
                return NotFound();
            }

            return Ok(dbEntity);
        }

        public IEnumerable<TEntity> HandleGetAction()
        {
            return _commonRepository.GetAll();
        }

        public async Task<IActionResult> HandleDeleteAction(TEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _commonRepository.Remove(entity))
            {
                return Ok();
            }

            //TODO.MK: Handle concurrency or bad request
            return NoContent();
        }
    }
}
