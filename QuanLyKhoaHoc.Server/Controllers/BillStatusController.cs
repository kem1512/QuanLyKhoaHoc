namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class BillStatusController : BaseController<BillStatusMapping, BillStatusQuery, BillStatusCreate, BillStatusUpdate>
    {
        public BillStatusController(ApplicationServiceBase<BillStatusMapping, BillStatusQuery, BillStatusCreate, BillStatusUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<BillStatusMapping>> GetEntities([FromQuery] BillStatusQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<BillStatusMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
