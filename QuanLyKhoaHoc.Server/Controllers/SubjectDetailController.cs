namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class SubjectDetailController : BaseController<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>
    {
        public SubjectDetailController(ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<SubjectDetailMapping>> GetEntities([FromQuery] SubjectDetailQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<SubjectDetailMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
