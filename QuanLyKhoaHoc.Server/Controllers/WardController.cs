using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardController : BaseController<WardMapping, WardQuery, WardCreate, WardUpdate>
    {
        public WardController(ApplicationServiceBase<WardMapping, WardQuery, WardCreate, WardUpdate> context)
            : base(context)
        {
        }
    }
}
