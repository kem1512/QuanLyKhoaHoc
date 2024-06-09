namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController<RoleMapping, RoleQuery, RoleCreate, RoleUpdate>
    {
        public RoleController(ApplicationServiceBase<RoleMapping, RoleQuery, RoleCreate, RoleUpdate> context)
            : base(context)
        {
        }
    }
}
