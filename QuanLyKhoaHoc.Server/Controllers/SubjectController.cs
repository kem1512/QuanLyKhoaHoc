using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : BaseController<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>
    {
        public SubjectController(ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate> context) : base(context)
        {
        }
    }
}
