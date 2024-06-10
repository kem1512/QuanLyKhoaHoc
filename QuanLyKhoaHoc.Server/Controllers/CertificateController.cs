
namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class CertificateController : BaseController<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate>
    {
        public CertificateController(ApplicationServiceBase<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate> context) : base(context)
        {
        }

        [AllowAnonymous]
        public override Task<PagingModel<CertificateMapping>> GetEntities([FromQuery] CertificateQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<CertificateMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
