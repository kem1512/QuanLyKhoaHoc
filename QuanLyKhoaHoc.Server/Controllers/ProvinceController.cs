namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class ProvinceController : BaseController<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate>
    {
        public ProvinceController(ApplicationServiceBase<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<ProvinceMapping>> GetEntities([FromQuery] ProvinceQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<ProvinceMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
