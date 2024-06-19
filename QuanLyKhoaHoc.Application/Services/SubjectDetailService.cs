using QuanLyKhoaHoc.Domain.Entities;

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
                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var subjectDetail = _mapper.Map<SubjectDetail>(entity);

                await _context.SubjectDetails.AddAsync(subjectDetail, cancellation);

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

                var subjectDetail = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (subjectDetail == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.SubjectDetails.Remove(subjectDetail);

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
            var subjectDetails = _context.SubjectDetails.AsNoTracking();

            var totalCount = await subjectDetails.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await subjectDetails
                .ApplyQuery(query)
                .ProjectTo<SubjectDetailMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<SubjectDetailMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<SubjectDetailMapping?> Get(int id, CancellationToken cancellation)
        {
            var subjectDetail = await _context.SubjectDetails.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == id, cancellation);

            if (subjectDetail == null)
            {
                return null;
            }

            subjectDetail.Subject.SubjectDetails = [];

            return _mapper.Map<SubjectDetailMapping>(subjectDetail);
        }

        public override async Task<Result> Update(int id, SubjectDetailUpdate entity, CancellationToken cancellation)
        {
            try
            {

                if (!_user.IsInstructorCertificate && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var subjectDetail = await _context.SubjectDetails.FindAsync(new object[] { id }, cancellation);

                if (subjectDetail == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.SubjectDetails.Update(_mapper.Map(entity, subjectDetail));

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
