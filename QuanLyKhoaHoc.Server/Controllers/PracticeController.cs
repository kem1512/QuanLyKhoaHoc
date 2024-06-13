namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class PracticeController : BaseController<PracticeMapping, PracticeQuery, PracticeCreate, PracticeUpdate>
    {
        public PracticeController(ApplicationServiceBase<PracticeMapping, PracticeQuery, PracticeCreate, PracticeUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<PracticeMapping>> GetEntities([FromQuery] PracticeQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<PracticeMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
