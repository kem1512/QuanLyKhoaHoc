namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class RegisterStudyController : BaseController<RegisterStudyMapping, RegisterStudyQuery, RegisterStudyCreate, RegisterStudyUpdate>
    {
        public RegisterStudyController(ApplicationServiceBase<RegisterStudyMapping, RegisterStudyQuery, RegisterStudyCreate, RegisterStudyUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<RegisterStudyMapping>> GetEntities([FromQuery] RegisterStudyQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<RegisterStudyMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
