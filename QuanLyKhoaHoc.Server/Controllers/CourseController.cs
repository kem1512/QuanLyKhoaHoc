using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>
    {
        public CourseController(ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate> context) : base(context)
        {
        }
    }
}
