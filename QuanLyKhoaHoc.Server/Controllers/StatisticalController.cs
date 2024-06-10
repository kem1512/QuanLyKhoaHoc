namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        [HttpGet]
        public bool Check()
        {
            return true;
        }
    }
}
