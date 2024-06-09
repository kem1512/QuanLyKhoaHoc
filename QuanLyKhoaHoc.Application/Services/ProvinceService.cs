namespace QuanLyKhoaHoc.Application.Services
{
    public class ProvinceService : ApplicationServiceBase<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate>
    {
        public ProvinceService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(ProvinceCreate entity, CancellationToken cancellation)
        {
            try
            {
                var province = _mapper.Map<Province>(entity);
                await _context.Provinces.AddAsync(province, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
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
                var province = await _context.Provinces.FindAsync(new object[] { id }, cancellation);

                if (province == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Provinces.Remove(province);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<PagingModel<ProvinceMapping>> Get(ProvinceQuery query, CancellationToken cancellation)
        {
            var provinces = _context.Provinces.AsNoTracking();

            var totalCount = await provinces.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await provinces
                .ApplyQuery(query)
                .ProjectTo<ProvinceMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<ProvinceMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<ProvinceMapping?> Get(int id, CancellationToken cancellation)
        {
            var province = await _context.Provinces.FindAsync(new object[] { id }, cancellation);

            if (province == null)
            {
                return null;
            }

            return _mapper.Map<ProvinceMapping>(province);
        }

        public override async Task<Result> Update(int id, ProvinceUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var province = await _context.Provinces.FindAsync(new object[] { id }, cancellation);

                if (province == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, province);

                _context.Provinces.Update(province);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
