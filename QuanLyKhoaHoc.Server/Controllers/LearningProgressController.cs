namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class LearningProgressController : BaseController<LearningProgressMapping, LearningProgressQuery, LearningProgressCreate, LearningProgressUpdate>
    {
        public LearningProgressController(ApplicationServiceBase<LearningProgressMapping, LearningProgressQuery, LearningProgressCreate, LearningProgressUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<LearningProgressMapping>> GetEntities([FromQuery] LearningProgressQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<LearningProgressMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
