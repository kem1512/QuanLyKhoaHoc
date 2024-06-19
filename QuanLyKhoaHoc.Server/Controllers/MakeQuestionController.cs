namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class MakeQuestionController : BaseController<MakeQuestionMapping, MakeQuestionQuery, MakeQuestionCreate, MakeQuestionUpdate>
    {
        public MakeQuestionController(ApplicationServiceBase<MakeQuestionMapping, MakeQuestionQuery, MakeQuestionCreate, MakeQuestionUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<MakeQuestionMapping>> GetEntities([FromQuery] MakeQuestionQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<MakeQuestionMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
