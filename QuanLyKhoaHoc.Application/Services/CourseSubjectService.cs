namespace QuanLyKhoaHoc.Application.Services
{
    public class CourseSubjectService : ApplicationServiceBase<CourseSubjectMapping, CourseSubjectQuery, CourseSubjectCreate, CourseSubjectUpdate>
    {
        public CourseSubjectService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CourseSubjectCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var courseSubject = _mapper.Map<CourseSubject>(entity);

                await _context.CourseSubjects.AddAsync(courseSubject, cancellation);

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

                var courseSubject = await _context.CourseSubjects.FindAsync(new object[] { id }, cancellation);

                if (courseSubject == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.CourseSubjects.Remove(courseSubject);

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

        public override async Task<PagingModel<CourseSubjectMapping>> Get(CourseSubjectQuery query, CancellationToken cancellation)
        {
            var courseSubjects = _context.CourseSubjects.AsNoTracking();

            if(query.CourseId != null)
            {
                courseSubjects = courseSubjects.Where(c => c.CourseId == query.CourseId);
            }

            var totalCount = await courseSubjects.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await courseSubjects
                .ApplyQuery(query)
                .ProjectTo<CourseSubjectMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CourseSubjectMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CourseSubjectMapping?> Get(int id, CancellationToken cancellation)
        {
            var courseSubject = await _context.CourseSubjects.FindAsync(new object[] { id }, cancellation);

            if (courseSubject == null)
            {
                return null;
            }

            return _mapper.Map<CourseSubjectMapping>(courseSubject);
        }

        public override async Task<Result> Update(int id, CourseSubjectUpdate entity, CancellationToken cancellation)
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

                var courseSubject = await _context.CourseSubjects.FindAsync(new object[] { id }, cancellation);

                if (courseSubject == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, courseSubject);

                _context.CourseSubjects.Update(courseSubject);

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
