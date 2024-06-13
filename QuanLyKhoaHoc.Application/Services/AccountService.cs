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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var user = await _context.Users.FindAsync(new object[] { int.Parse(_user.Id) }, cancellation);

                if (user == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (user.Id != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
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

        public async Task<UserInfo?> UserInfo(CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Province).Include(c => c.District).Include(c => c.Ward).AsNoTracking().FirstOrDefaultAsync(c => c.Id.ToString() == _user.Id, cancellation);

            if (user == null) return null;

            return _mapper.Map<UserInfo>(user);
        }
    }
}
