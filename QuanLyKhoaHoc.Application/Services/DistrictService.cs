namespace QuanLyKhoaHoc.Application.Services
{
    public class DistrictService : ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>
    {
        public DistrictService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(DistrictCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Tạo");
                }

                var district = _mapper.Map<District>(entity);

                await _context.Districts.AddAsync(district, cancellation);

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

                var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

                if (district == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Districts.Remove(district);

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

        public override async Task<PagingModel<DistrictMapping>> Get(DistrictQuery query, CancellationToken cancellation)
        {
            var districts = _context.Districts.AsNoTracking();

            if(query.ProvinceId != null)
            {
                districts = districts.Where(c => c.ProvinceId == query.ProvinceId);
            }

            var totalCount = await districts.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await districts
                .ApplyQuery(query)
                .ProjectTo<DistrictMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<DistrictMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<DistrictMapping?> Get(int id, CancellationToken cancellation)
        {
            var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

            if (district == null)
            {
                return null;
            }

            return _mapper.Map<DistrictMapping>(district);
        }

        public override async Task<Result> Update(int id, DistrictUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

                if (district == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, district);

                _context.Districts.Update(district);

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
