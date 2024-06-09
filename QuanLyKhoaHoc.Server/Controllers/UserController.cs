using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserMapping, UserQuery, UserCreate, UserUpdate>
    {
        public UserController(ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate> context)
            : base(context)
        {
        }
    }
}
