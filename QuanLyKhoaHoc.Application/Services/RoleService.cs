namespace QuanLyKhoaHoc.Application.Services
{
    public class RoleService : ApplicationServiceBase<RoleMapping, RoleQuery, RoleCreate, RoleUpdate>
    {
        public RoleService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(RoleCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var role = _mapper.Map<Role>(entity);


                await _context.Roles.AddAsync(role, cancellation);

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
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var role = await _context.Roles.FindAsync(new object[] { id }, cancellation);

                if (role == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Roles.Remove(role);

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

        public override async Task<PagingModel<RoleMapping>> Get(RoleQuery query, CancellationToken cancellation)
        {
            var roles = _context.Roles.AsNoTracking();

            var totalCount = await roles.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await roles
                .ApplyQuery(query)
                .ProjectTo<RoleMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<RoleMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<RoleMapping?> Get(int id, CancellationToken cancellation)
        {
            var role = await _context.Roles.FindAsync(new object[] { id }, cancellation);

            if (role == null)
            {
                return null;
            }

            return _mapper.Map<RoleMapping>(role);
        }

        public override async Task<Result> Update(int id, RoleUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var role = await _context.Roles.FindAsync(new object[] { id }, cancellation);

                if (role == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Roles.Update(_mapper.Map(entity, role));

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
