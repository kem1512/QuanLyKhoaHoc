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
