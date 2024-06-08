using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Application;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TMapping, TQuery, TCreate, TUpdate> : ControllerBase
        where TMapping : class
        where TQuery : class
        where TCreate : class
        where TUpdate : class
    {
        private readonly ApplicationServiceBase<TMapping, TQuery, TCreate, TUpdate> _context;

        protected BaseController(ApplicationServiceBase<TMapping, TQuery, TCreate, TUpdate> context)
        {
            _context = context;
        }

        [HttpGet]
        public virtual async Task<PagingModel<TMapping>> GetEntities([FromQuery] TQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public virtual async Task<TMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }

        [HttpPost]
        public virtual async Task<Result> CreateEntity(TCreate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }

        [HttpPut("{id}")]
        public virtual async Task<Result> UpdateEntity(int id, TUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete("{id}")]
        public virtual async Task<Result> DeleteEntity(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
