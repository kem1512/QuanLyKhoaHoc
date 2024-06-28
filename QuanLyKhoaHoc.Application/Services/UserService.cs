namespace QuanLyKhoaHoc.Application.Services
{
    public class UserService : ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate>
    {
        public UserService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(UserCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var user = _mapper.Map<User>(entity);

                user.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

                await _context.Users.AddAsync(user, cancellation);

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

        public override async Task<Result> Delete(int id, CancellationToken cancellation)
        {
            try
            {
                var user = await _context.Users.FindAsync(new object[] { id }, cancellation);

                if (user == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }


                if (user.Id.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.Users.Remove(user);

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

        public override async Task<PagingModel<UserMapping>> Get(UserQuery query, CancellationToken cancellation)
        {
            var users = _context.Users.AsNoTracking();

            var totalCount = await users.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await users
                .ApplyQuery(query)
                .ProjectTo<UserMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<UserMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<UserMapping?> Get(int id, CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Province).Include(c => c.District).Include(c => c.Ward).Include(c => c.Permissions).ThenInclude(c => c.Role).Include(c => c.Certificate).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellation);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserMapping>(user);
        }

        public override async Task<Result> Update(int id, UserUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var user = await _context.Users.Include(c => c.Permissions).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (user == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (user.Id.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                var currentPermissionIds = user.Permissions.Select(cs => cs.Id).ToList();

                var updatedPermissionIds = entity.Permissions.Select(cs => cs.Id).ToList();

                var permissionsToRemove = user.Permissions
                    .Where(cs => !updatedPermissionIds.Contains(cs.Id))
                    .ToList();

                _context.Permissions.RemoveRange(permissionsToRemove);

                user.UpdateTime = DateTime.UtcNow;

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
    }
}
