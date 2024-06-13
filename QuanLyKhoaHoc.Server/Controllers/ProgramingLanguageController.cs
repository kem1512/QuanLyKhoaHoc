
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class ProgramingLanguageController : BaseController<ProgramingLanguageMapping, ProgramingLanguageQuery, ProgramingLanguageCreate, ProgramingLanguageUpdate>
    {
        public ProgramingLanguageController(ApplicationServiceBase<ProgramingLanguageMapping, ProgramingLanguageQuery, ProgramingLanguageCreate, ProgramingLanguageUpdate> context)
            : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<ProgramingLanguageMapping>> GetEntities([FromQuery] ProgramingLanguageQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<ProgramingLanguageMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
