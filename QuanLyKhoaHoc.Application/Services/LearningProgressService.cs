using static QuanLyKhoaHoc.Application.Common.Mapping.LearningProgressQuery;

namespace QuanLyKhoaHoc.Application.Services
{
    public class LearningProgressService : ApplicationServiceBase<LearningProgressMapping, LearningProgressQuery, LearningProgressCreate, LearningProgressUpdate>
    {
        public LearningProgressService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(LearningProgressCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var LearningProgress = _mapper.Map<LearningProgress>(entity);

                await _context.LearningProgress.AddAsync(LearningProgress, cancellation);

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

                var LearningProgress = await _context.LearningProgress.FindAsync(new object[] { id }, cancellation);

                if (LearningProgress == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.LearningProgress.Remove(LearningProgress);

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

        public override async Task<PagingModel<LearningProgressMapping>> Get(LearningProgressQuery query, CancellationToken cancellation)
        {
            var learningProgress = _context.LearningProgress.AsNoTracking();

            if(query.RegisterStudyId != null)
            {
                learningProgress = learningProgress.Where(c => c.CurrentSubjectId == query.RegisterStudyId && c.UserId.ToString() == _user.Id);
            }

            var totalCount = await learningProgress.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await learningProgress
                .ApplyQuery(query)
                .ProjectTo<LearningProgressMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<LearningProgressMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<LearningProgressMapping?> Get(int id, CancellationToken cancellation)
        {
            var learningProgress = await _context.LearningProgress.FindAsync(new object[] { id }, cancellation);

            if (learningProgress == null)
            {
                return null;
            }

            return _mapper.Map<LearningProgressMapping>(learningProgress);
        }

        public override async Task<Result> Update(int id, LearningProgressUpdate entity, CancellationToken cancellation)
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

                var LearningProgress = await _context.LearningProgress.FindAsync(new object[] { id }, cancellation);

                if (LearningProgress == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, LearningProgress);

                _context.LearningProgress.Update(LearningProgress);

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
