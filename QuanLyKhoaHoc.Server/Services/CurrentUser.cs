namespace QuanLyKhoaHoc.Server.Services;
public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");

    public bool IsAdministrator => _httpContextAccessor.HttpContext?.User?.IsInRole(Roles.Administrator) ?? false;

    public bool IsInstructorCertificate => _httpContextAccessor.HttpContext?.User.FindFirst("Certificate")?.Value.Contains(Certificates.InstructorCertificate) ?? false;

}
