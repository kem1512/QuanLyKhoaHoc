namespace QuanLyKhoaHoc.Application.Services
{
    public class SubjectService : ApplicationServiceBase<SubjectMapping, SubjectQuery, SubjectCreate, SubjectUpdate>
    {
        public SubjectService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(SubjectCreate entity, CancellationToken cancellation)
        {
            try
            {

                if ( !_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var subject = _mapper.Map<Subject>(entity);

                await _context.Subjects.AddAsync(subject, cancellation);

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

                var subject = await _context.Subjects.FindAsync(new object[] { id }, cancellation);

                if (subject == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Subjects.Remove(subject);

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

        public override async Task<PagingModel<SubjectMapping>> Get(SubjectQuery query, CancellationToken cancellation)
        {
            var subjects = _context.Subjects.AsNoTracking();

            var totalCount = await subjects.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await subjects
                .ApplyQuery(query)
                .ProjectTo<SubjectMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<SubjectMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<SubjectMapping?> Get(int id, CancellationToken cancellation)
        {
            var subject = await _context.Subjects.FindAsync(new object[] { id }, cancellation);

            if (subject == null)
            {
                return null;
            }

            return _mapper.Map<SubjectMapping>(subject);
        }

        public override async Task<Result> Update(int id, SubjectUpdate entity, CancellationToken cancellation)
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

                var subject = await _context.Subjects.FindAsync(new object[] { id }, cancellation);

                if (subject == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, subject);

                _context.Subjects.Update(subject);

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
