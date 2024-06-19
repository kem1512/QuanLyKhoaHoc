namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class AnswersController : BaseController<AnswersMapping, AnswersQuery, AnswersCreate, AnswersUpdate>
    {
        public AnswersController(ApplicationServiceBase<AnswersMapping, AnswersQuery, AnswersCreate, AnswersUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<AnswersMapping>> GetEntities([FromQuery] AnswersQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<AnswersMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
