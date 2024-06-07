using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate> _context;

        public SubjectController(ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<PagingModel<SubjectMapping>> GetSubjects([FromQuery] SubjectQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<SubjectMapping?> GetSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Result> CreateSubject(SubjectCreate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }

        [HttpPut]
        public async Task<Result> UpdateSubject(int id, SubjectUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteSubject(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
