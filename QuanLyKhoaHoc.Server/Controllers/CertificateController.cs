namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : BaseController<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate>
    {
        public CertificateController(ApplicationServiceBase<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate> context) : base(context)
        {
        }
    }
}
