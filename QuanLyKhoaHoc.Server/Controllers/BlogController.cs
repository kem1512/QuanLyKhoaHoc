
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class BlogController : BaseController<BlogMapping, BlogQuery, BlogCreate, BlogUpdate>
    {
        public BlogController(ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<BlogMapping>> GetEntities([FromQuery] BlogQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<BlogMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
