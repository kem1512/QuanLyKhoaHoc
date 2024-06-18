using Azure.Core;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    [ApiController]
    public class BillController : BaseController<BillMapping, BillQuery, BillCreate, BillUpdate>
    {
        private readonly IConfiguration _configuration;

        public BillController(ApplicationServiceBase<BillMapping, BillQuery, BillCreate, BillUpdate> context, IConfiguration configuration)
            : base(context)
        {
            _configuration = configuration;
        }

        public override async Task<Result> CreateEntity(BillCreate entity, CancellationToken cancellationToken)
        {
            var vnpUrl = _configuration["VNPaySettings:vnp_Url"];
            var vnpTmnCode = _configuration["VNPaySettings:vnp_TmnCode"];
            var vnpHashSecret = _configuration["VNPaySettings:vnp_HashSecret"];
            var vnpReturnUrl = _configuration["VNPaySettings:vnp_ReturnUrl"];

            if (string.IsNullOrEmpty(vnpUrl) || string.IsNullOrEmpty(vnpTmnCode) || string.IsNullOrEmpty(vnpHashSecret) || string.IsNullOrEmpty(vnpReturnUrl))
            {
                return await Task.FromResult(Result.Failure("Chưa Cấu Hình VNPay"));
            }

            entity.VNPaySettings = new VNPaySettings(vnpUrl, vnpTmnCode, vnpHashSecret, vnpReturnUrl, HttpContext.Connection.RemoteIpAddress?.ToString());

            return await base.CreateEntity(entity, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<PagingModel<BillMapping>> GetEntities([FromQuery] BillQuery query, CancellationToken cancellationToken)
        {
            return base.GetEntities(query, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<BillMapping?> GetEntity(int id, CancellationToken cancellationToken)
        {
            return base.GetEntity(id, cancellationToken);
        }
    }
}
