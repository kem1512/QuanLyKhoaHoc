namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class UserController : BaseController<UserMapping, UserQuery, UserCreate, UserUpdate>
    {
        public UserController(ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<UserMapping>> GetEntities([FromQuery] UserQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<UserMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
