namespace QuanLyKhoaHoc.Application.Services
{
    public class AnswersService : ApplicationServiceBase<AnswersMapping, AnswersQuery, AnswersCreate, AnswersUpdate>
    {
        public AnswersService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(AnswersCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var answers = _mapper.Map<Answers>(entity);

                answers.UserId = int.Parse(_user.Id);

                await _context.Answers.AddAsync(answers, cancellation);

                var question = await _context.MakeQuestions.FirstOrDefaultAsync(c => c.Id == entity.QuestionId);

                if (question != null) {
                    question.NumberOfAnswers += 1;
                }

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
                var answers = await _context.Answers.AsNoTracking().Include(c => c.Question).FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (answers == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (answers.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.Answers.Remove(answers);

                if (answers.Question.NumberOfAnswers >= 1)
                {
                    answers.Question.NumberOfAnswers -= 1;
                }

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

        public override async Task<PagingModel<AnswersMapping>> Get(AnswersQuery query, CancellationToken cancellation)
        {
            var answers = _context.Answers.AsNoTracking();

            if (query.QuestionId != null)
            {
                answers = answers.Where(c => c.QuestionId == query.QuestionId);
            }

            var totalCount = await answers.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await answers
                .ApplyQuery(query)
                .ProjectTo<AnswersMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<AnswersMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<AnswersMapping?> Get(int id, CancellationToken cancellation)
        {
            var commentAnswers = await _context.Answers.FindAsync(id, cancellation);

            if (commentAnswers == null)
            {
                return null;
            }

            return _mapper.Map<AnswersMapping>(commentAnswers);
        }

        public override async Task<Result> Update(int id, AnswersUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var answers = await _context.Answers.FindAsync(id, cancellation);

                if (answers == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (answers.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.Answers.Update(_mapper.Map(entity, answers));

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
