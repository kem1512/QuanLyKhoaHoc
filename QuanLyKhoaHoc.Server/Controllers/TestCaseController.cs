namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class TestCaseController : BaseController<TestCaseMapping, TestCaseQuery, TestCaseCreate, TestCaseUpdate>
    {
        public TestCaseController(ApplicationServiceBase<TestCaseMapping, TestCaseQuery, TestCaseCreate, TestCaseUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<TestCaseMapping>> GetEntities([FromQuery] TestCaseQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<TestCaseMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
