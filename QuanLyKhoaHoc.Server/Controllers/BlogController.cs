using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate> _context;

        public BlogController(ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<PagingModel<BlogMapping>> GetBlogs([FromQuery] BlogQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<BlogMapping?> GetBlog(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Result> CreateBlog(BlogCreate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }

        [HttpPut]
        public async Task<Result> UpdateBlog(int id, BlogUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteBlog(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
