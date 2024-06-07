using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate> _context;

        public CourseController(ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<PagingModel<CourseMapping>> GetCourses([FromQuery] CourseQuery query, CancellationToken cancellationToken)
        {
            return await _context.Get(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<CourseMapping?> GetCourse(int id, CancellationToken cancellationToken)
        {
            return await _context.Get(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Result> CreateCourse(CourseCreate entity, CancellationToken cancellationToken)
        {
            return await _context.Create(entity, cancellationToken);
        }

        [HttpPut]
        public async Task<Result> UpdateCourse(int id, CourseUpdate entity, CancellationToken cancellationToken)
        {
            return await _context.Update(id, entity, cancellationToken);
        }

        [HttpDelete]
        public async Task<Result> DeleteCourse(int id, CancellationToken cancellationToken)
        {
            return await _context.Delete(id, cancellationToken);
        }
    }
}
