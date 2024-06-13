
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class CommentBlogController : BaseController<CommentBlogMapping, CommentBlogQuery, CommentBlogCreate, CommentBlogUpdate>
    {
        public CommentBlogController(ApplicationServiceBase<CommentBlogMapping, CommentBlogQuery, CommentBlogCreate, CommentBlogUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<CommentBlogMapping>> GetEntities([FromQuery] CommentBlogQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<CommentBlogMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
