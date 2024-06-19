namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class CourseSubjectController : BaseController<CourseSubjectMapping, CourseSubjectQuery, CourseSubjectCreate, CourseSubjectUpdate>
    {
        public CourseSubjectController(ApplicationServiceBase<CourseSubjectMapping, CourseSubjectQuery, CourseSubjectCreate, CourseSubjectUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<CourseSubjectMapping>> GetEntities([FromQuery] CourseSubjectQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<CourseSubjectMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
