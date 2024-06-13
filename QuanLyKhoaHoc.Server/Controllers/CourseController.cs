namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<CourseMapping, CourseQuery, CourseCreate, CourseUpdate>
    {
        public CourseController(ApplicationServiceBase<CourseMapping, CourseQuery, CourseCreate, CourseUpdate> context) : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<CourseMapping>> GetEntities([FromQuery] CourseQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<CourseMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
