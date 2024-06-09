using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateTypeController : BaseController<CertificateTypeMapping, CertificateTypeQuery, CertificateTypeCreate, CertificateTypeUpdate>
    {
        public CertificateTypeController(ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CertificateTypeCreate, CertificateTypeUpdate> context) : base(context)
        {
        }
    }
}
