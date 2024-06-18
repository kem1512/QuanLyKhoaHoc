namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class SubjectController : BaseController<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>
    {
        public SubjectController(ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<SubjectMapping>> GetEntities([FromQuery] SubjectQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<SubjectMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
