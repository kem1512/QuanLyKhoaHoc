namespace QuanLyKhoaHoc.Application.Services
{
    public class WardService : ApplicationServiceBase<WardMapping, WardQuery, WardCreate, WardUpdate>
    {
        public WardService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(WardCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var ward = _mapper.Map<Ward>(entity);

                await _context.Wards.AddAsync(ward, cancellation);

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
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var ward = await _context.Wards.FindAsync(new object[] { id }, cancellation);

                if (ward == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Wards.Remove(ward);

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

        public override async Task<PagingModel<WardMapping>> Get(WardQuery query, CancellationToken cancellation)
        {
            var wards = _context.Wards.AsNoTracking();

            if (query.DistrictId != null)
            {
                wards = wards.Where(c => c.Id == query.DistrictId);
            }

            var totalCount = await wards.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await wards
                .ApplyQuery(query)
                .ProjectTo<WardMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<WardMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<WardMapping?> Get(int id, CancellationToken cancellation)
        {
            var ward = await _context.Wards.FindAsync(new object[] { id }, cancellation);

            if (ward == null)
            {
                return null;
            }

            return _mapper.Map<WardMapping>(ward);
        }

        public override async Task<Result> Update(int id, WardUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var ward = await _context.Wards.FindAsync(new object[] { id }, cancellation);

                if (ward == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, ward);

                _context.Wards.Update(ward);

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
