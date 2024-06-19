namespace QuanLyKhoaHoc.Application.Services
{
    public class PracticeService : ApplicationServiceBase<PracticeMapping, PracticeQuery, PracticeCreate, PracticeUpdate>
    {
        public PracticeService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(PracticeCreate entity, CancellationToken cancellation)
        {
            try
            {

                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var practice = _mapper.Map<Practice>(entity);

                await _context.Practices.AddAsync(practice, cancellation);

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

                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var practice = await _context.Practices.FindAsync(new object[] { id }, cancellation);

                if (practice == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Practices.Remove(practice);

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

        public override async Task<PagingModel<PracticeMapping>> Get(PracticeQuery query, CancellationToken cancellation)
        {
            var practices = _context.Practices.AsNoTracking();

            var totalCount = await practices.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await practices
                .ApplyQuery(query)
                .ProjectTo<PracticeMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<PracticeMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<PracticeMapping?> Get(int id, CancellationToken cancellation)
        {
            var practice = await _context.Practices.FindAsync(new object[] { id }, cancellation);

            if (practice == null)
            {
                return null;
            }

            return _mapper.Map<PracticeMapping>(practice);
        }

        public override async Task<Result> Update(int id, PracticeUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var practice = await _context.Practices.FindAsync(new object[] { id }, cancellation);

                if (practice == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                _mapper.Map(entity, practice);

                _context.Practices.Update(practice);

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
