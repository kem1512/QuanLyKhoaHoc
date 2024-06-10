
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class DistrictController : BaseController<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>
    {
        public DistrictController(ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<DistrictMapping>> GetEntities([FromQuery] DistrictQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<DistrictMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
