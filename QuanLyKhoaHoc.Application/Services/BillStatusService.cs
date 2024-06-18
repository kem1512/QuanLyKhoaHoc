namespace QuanLyKhoaHoc.Application.Services
{
    public class BillStatusService : ApplicationServiceBase<BillStatusMapping, BillStatusQuery, BillStatusCreate, BillStatusUpdate>
    {
        public BillStatusService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(BillStatusCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var billStatus = _mapper.Map<BillStatus>(entity);

                await _context.BillStatus.AddAsync(billStatus, cancellation);

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

                var billStatus = await _context.BillStatus.FindAsync(new object[] { id }, cancellation);

                if (billStatus == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.BillStatus.Remove(billStatus);

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

        public override async Task<PagingModel<BillStatusMapping>> Get(BillStatusQuery query, CancellationToken cancellation)
        {
            var billStatuss = _context.BillStatus.AsNoTracking();

            var totalCount = await billStatuss.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await billStatuss
                .ApplyQuery(query)
                .ProjectTo<BillStatusMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<BillStatusMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<BillStatusMapping?> Get(int id, CancellationToken cancellation)
        {
            var billStatus = await _context.BillStatus.FindAsync(new object[] { id }, cancellation);

            if (billStatus == null)
            {
                return null;
            }

            return _mapper.Map<BillStatusMapping>(billStatus);
        }

        public override async Task<Result> Update(int id, BillStatusUpdate entity, CancellationToken cancellation)
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

                var billStatus = await _context.BillStatus.FindAsync(new object[] { id }, cancellation);

                if (billStatus == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, billStatus);

                _context.BillStatus.Update(billStatus);

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
