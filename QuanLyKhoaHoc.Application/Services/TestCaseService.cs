using QuanLyKhoaHoc.Domain.Entities;

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


                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var testCase = _mapper.Map<TestCase>(entity);

                await _context.TestCases.AddAsync(testCase, cancellation);

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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");


                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var testCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

                if (testCase == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.TestCases.Remove(testCase);

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

        public override async Task<PagingModel<TestCaseMapping>> Get(TestCaseQuery query, CancellationToken cancellation)
        {
            var testCases = _context.TestCases.AsNoTracking();

            var totalCount = await testCases.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await testCases
                .ApplyQuery(query)
                .ProjectTo<TestCaseMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<TestCaseMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<TestCaseMapping?> Get(int id, CancellationToken cancellation)
        {
            var testCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

            if (testCase == null)
            {
                return null;
            }

            return _mapper.Map<TestCaseMapping>(testCase);
        }

        public override async Task<Result> Update(int id, TestCaseUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");


                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var testCase = await _context.TestCases.FindAsync(new object[] { id }, cancellation);

                if (testCase == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.TestCases.Update(_mapper.Map(entity, testCase));

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
