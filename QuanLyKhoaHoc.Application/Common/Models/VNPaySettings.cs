namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class VNPaySettings
    {
        public string vnp_Url;

        public string vnp_TmnCode;

        public string vnp_HashSecret;

        public string vnp_ReturnUrl;

        public string? ip;

        public VNPaySettings(string vnp_Url, string vnp_TmnCode, string vnp_HashSecret, string vnp_ReturnUrl, string? ip)
        {
            this.vnp_Url = vnp_Url;
            this.vnp_TmnCode = vnp_TmnCode;
            this.vnp_HashSecret = vnp_HashSecret;
            this.vnp_ReturnUrl = vnp_ReturnUrl;
            this.ip = ip;
        }
    }
}
