using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyKhoaHoc.Infrastructure.Data;

public class AddressData
{
    public Province[] Data { get; set; }

    public class Province
    {
        public string Name { get; set; }
        public District[] Data2 { get; set; }
    }

    public class District
    {
        public string Name { get; set; }
        public Ward[] Data3 { get; set; }
    }

    public class Ward
    {
        public string Name { get; set; }
    }
}

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Post()
        {
            try
            {
                HttpClient client = new HttpClient();

                // Gửi yêu cầu GET đến API để lấy dữ liệu
                var response = await client.GetAsync("https://esgoo.net/api-tinhthanh/4/0.htm");

                if (!response.IsSuccessStatusCode)
                {
                    // Xử lý nếu yêu cầu không thành công
                    return StatusCode((int)response.StatusCode);
                }

                // Đọc nội dung của phản hồi
                var jsonString = await response.Content.ReadAsStringAsync();

                // Deserialize JSON thành mảng các tỉnh/thành phố
                var address = JsonConvert.DeserializeObject<AddressData>(jsonString);

                // Thêm dữ liệu x cơ sở dữ liệu
                foreach (var x in address.Data)
                {
                    var province = new Domain.Entities.Province() { Name = x.Name };

                    province.Districts = new List<Domain.Entities.District>();

                    foreach (var y in x.Data2)
                    {
                        var district = new Domain.Entities.District() { Name = y.Name };

                        district.Wards = new List<Domain.Entities.Ward>();

                        province.Districts.Add(district);

                        foreach (var z in y.Data3)
                        {
                            var ward = new Domain.Entities.Ward() { Name = z.Name };

                            district.Wards.Add(ward);
                        }
                    }

                    _context.Provinces.Add(province);
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
