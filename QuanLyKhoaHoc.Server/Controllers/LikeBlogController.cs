namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class LikeBlogController : BaseController<LikeBlogMapping, LikeBlogQuery, LikeBlogCreate, LikeBlogUpdate>
    {
        public LikeBlogController(ApplicationServiceBase<LikeBlogMapping, LikeBlogQuery, LikeBlogCreate, LikeBlogUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<LikeBlogMapping>> GetEntities([FromQuery] LikeBlogQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<LikeBlogMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
