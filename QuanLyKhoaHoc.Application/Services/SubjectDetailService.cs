namespace QuanLyKhoaHoc.Application.Services
{
    public class SubjectDetailService : ApplicationServiceBase<SubjectDetailMapping, SubjectDetailQuery, SubjectDetailCreate, SubjectDetailUpdate>
    {
        public SubjectDetailService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(SubjectDetailCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");


                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var blog = _mapper.Map<SubjectDetail>(entity);

                await _context.SubjectDetails.AddAsync(blog, cancellation);

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

                var blog = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (blog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.SubjectDetails.Remove(blog);

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

        public override async Task<PagingModel<SubjectDetailMapping>> Get(SubjectDetailQuery query, CancellationToken cancellation)
        {
            var blogs = _context.SubjectDetails.AsNoTracking();

            var totalCount = await blogs.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await blogs
                .ApplyQuery(query)
                .ProjectTo<SubjectDetailMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<SubjectDetailMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<SubjectDetailMapping?> Get(int id, CancellationToken cancellation)
        {
            var blog = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

            if (blog == null)
            {
                return null;
            }

            return _mapper.Map<SubjectDetailMapping>(blog);
        }

        public override async Task<Result> Update(int id, SubjectDetailUpdate entity, CancellationToken cancellation)
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

                var blog = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (blog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.SubjectDetails.Update(_mapper.Map(entity, blog));

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
