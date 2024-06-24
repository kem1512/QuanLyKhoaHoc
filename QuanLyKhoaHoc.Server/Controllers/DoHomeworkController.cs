using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class DoHomeworkController : BaseController<DoHomeworkMapping, DoHomeworkQuery, DoHomeworkCreate, DoHomeworkUpdate>
    {
        public DoHomeworkController(ApplicationServiceBase<DoHomeworkMapping, DoHomeworkQuery, DoHomeworkCreate, DoHomeworkUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<DoHomeworkMapping>> GetEntities([FromQuery] DoHomeworkQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<DoHomeworkMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
