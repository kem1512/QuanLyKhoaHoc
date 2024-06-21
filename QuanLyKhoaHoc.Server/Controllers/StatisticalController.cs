namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalService _statisticalService;

        public StatisticalController(IStatisticalService statisticalService)
        {
            _statisticalService = statisticalService;
        }

        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue(DateTime? startDate, DateTime? endDate)
        {
            var totalRevenue = await _statisticalService.TotalRevenueAsync(startDate, endDate);
            return Ok(totalRevenue);
        }

        [HttpGet("top-selling-courses")]
        public async Task<IActionResult> GetTopSellingCourses(DateTime? startDate, DateTime? endDate, int topN)
        {
            var courses = await _statisticalService.TopSellingCoursesAsync(startDate, endDate, topN);
            return Ok(courses);
        }
    }
}
