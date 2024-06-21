namespace QuanLyKhoaHoc.Application.Services
{
    public class MakeQuestionService : ApplicationServiceBase<MakeQuestionMapping, MakeQuestionQuery, MakeQuestionCreate, MakeQuestionUpdate>
    {
        public MakeQuestionService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(MakeQuestionCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var makeQuestion = _mapper.Map<MakeQuestion>(entity);

                makeQuestion.UserId = int.Parse(_user.Id);

                await _context.MakeQuestions.AddAsync(makeQuestion, cancellation);

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
                var makeQuestion = await _context.MakeQuestions.FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (makeQuestion == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (makeQuestion.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.MakeQuestions.Remove(makeQuestion);

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

        public override async Task<PagingModel<MakeQuestionMapping>> Get(MakeQuestionQuery query, CancellationToken cancellation)
        {
            var makeQuestion = _context.MakeQuestions.AsNoTracking();

            if(query.SubjectDetailId != null)
            {
                makeQuestion = makeQuestion.Where(c => c.SubjectDetailId == query.SubjectDetailId);
            }

            var totalCount = await makeQuestion.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await makeQuestion
                .ApplyQuery(query)
                .ProjectTo<MakeQuestionMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<MakeQuestionMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<MakeQuestionMapping?> Get(int id, CancellationToken cancellation)
        {
            var commentMakeQuestion = await _context.MakeQuestions.FindAsync(new object[] { id }, cancellation);

            if (commentMakeQuestion == null)
            {
                return null;
            }

            return _mapper.Map<MakeQuestionMapping>(commentMakeQuestion);
        }

        public override async Task<Result> Update(int id, MakeQuestionUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var makeQuestion = await _context.MakeQuestions.FindAsync(new object[] { id }, cancellation);

                if (makeQuestion == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (makeQuestion.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.MakeQuestions.Update(_mapper.Map(entity, makeQuestion));

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
