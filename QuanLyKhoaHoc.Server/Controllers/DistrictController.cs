namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : BaseController<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>
    {
        public DistrictController(ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate> context)
            : base(context)
        {
        }
    }
}
