namespace QuanLyKhoaHoc.Application.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountService(IApplicationDbContext context, IUser user, IMapper mapper, ITokenService tokenService)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Result> UserInfoUpdate(UserInfoUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var user = await _context.Users.FindAsync(new object[] { int.Parse(_user.Id) }, cancellation);

                if (user == null || user.Id != int.Parse(_user.Id))
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.Users.Update(_mapper.Map(entity, user));

                var result = await _context.SaveChangesAsync(cancellation);

                if (result > 0)
                {
                    return Result.Success();
                }

                return Result.Failure();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> ChangePassword(string currentPassword, string newPassword, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var user = await _context.Users.FindAsync(new object[] { int.Parse(_user.Id) }, cancellation);

                if (user == null || user.Id != int.Parse(_user.Id))
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
                {
                    return new Result(ResultStatus.Forbidden, "Mật Khẩu Bạn Nhập Không Đúng");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

                _context.Users.Update(user);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result > 0)
                {
                    return Result.Success();
                }

                return Result.Failure();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<UserInfo?> UserInfo(CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Province).Include(c => c.District).Include(c => c.Ward).AsNoTracking().FirstOrDefaultAsync(c => c.Id.ToString() == _user.Id, cancellation);

            if (user == null) return null;

            return _mapper.Map<UserInfo>(user);
        }

        public async Task<RegisterStudyMapping?> RegisterStudy(int courseId, CancellationToken cancellation)
        {
            var registerStudy = await _context.RegisterStudys.FirstOrDefaultAsync(c => c.CourseId == courseId);

            if (registerStudy == null)
            {
                return null;
            }

            return _mapper.Map<RegisterStudyMapping>(registerStudy);
        }
    }
}
