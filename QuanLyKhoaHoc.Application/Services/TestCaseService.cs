namespace QuanLyKhoaHoc.Application.Services
{
    public class TestCaseService : ApplicationServiceBase<TestCaseMapping, TestCaseQuery, TestCaseCreate, TestCaseUpdate>
    {
        public TestCaseService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(TestCaseCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var TestCase = _mapper.Map<TestCase>(entity);

                await _context.TestCases.AddAsync(TestCase, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var TestCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

                if (TestCase == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.TestCases.Remove(TestCase);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<PagingModel<TestCaseMapping>> Get(TestCaseQuery query, CancellationToken cancellation)
        {
            var TestCases = _context.TestCases.AsNoTracking();

            var totalCount = await TestCases.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await TestCases
                .ApplyQuery(query)
                .ProjectTo<TestCaseMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<TestCaseMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<TestCaseMapping?> Get(int id, CancellationToken cancellation)
        {
            var TestCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

            if (TestCase == null)
            {
                return null;
            }

            return _mapper.Map<TestCaseMapping>(TestCase);
        }

        public override async Task<Result> Update(int id, TestCaseUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var TestCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

                if (TestCase == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.TestCases.Update(_mapper.Map(entity, TestCase));

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
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
