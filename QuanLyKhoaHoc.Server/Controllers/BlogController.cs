using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController<BlogMapping, BlogQuery, BlogCreate, BlogUpdate>
    {
        public BlogController(ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate> context)
            : base(context)
        {
        }
    }
}
