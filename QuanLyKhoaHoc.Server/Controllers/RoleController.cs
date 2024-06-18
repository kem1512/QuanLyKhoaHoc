namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class RoleController : BaseController<RoleMapping, RoleQuery, RoleCreate, RoleUpdate>
    {
        public RoleController(ApplicationServiceBase<RoleMapping, RoleQuery, RoleCreate, RoleUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<RoleMapping>> GetEntities([FromQuery] RoleQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<RoleMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
