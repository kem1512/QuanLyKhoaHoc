namespace QuanLyKhoaHoc.Application.Services
{
    public class RegisterStudyService : ApplicationServiceBase<RegisterStudyMapping, RegisterStudyQuery, RegisterStudyCreate, RegisterStudyUpdate>
    {
        public RegisterStudyService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(RegisterStudyCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var registerStudy = _mapper.Map<RegisterStudy>(entity);

                registerStudy.UserId = int.Parse(_user.Id);

                await _context.RegisterStudys.AddAsync(registerStudy, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                await _context.LearningProgress.AddAsync(new LearningProgress() { RegisterStudyId = registerStudy.Id, UserId = int.Parse(_user.Id), CurrentSubjectId = entity.CurrentSubjectId });

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
                var registerStudy = await _context.RegisterStudys.FindAsync(new object[] { id }, cancellation);

                if (registerStudy == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (registerStudy.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.RegisterStudys.Remove(registerStudy);

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

        public override async Task<PagingModel<RegisterStudyMapping>> Get(RegisterStudyQuery query, CancellationToken cancellation)
        {
            var registerStudies = _context.RegisterStudys.AsNoTracking();

            if(query.CourseId != null)
            {
                registerStudies = registerStudies.Where(c => c.CourseId  == query.CourseId && c.UserId.ToString() == _user.Id);
            }

            if(query.UserId != null)
            {
                registerStudies = registerStudies.Where(c => c.UserId == query.UserId);
            }

            var totalCount = await registerStudies.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await registerStudies
                .ApplyQuery(query)
                .ProjectTo<RegisterStudyMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<RegisterStudyMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<RegisterStudyMapping?> Get(int id, CancellationToken cancellation)
        {
            var registerStudy = await _context.RegisterStudys.FindAsync(new object[] { id }, cancellation);

            if (registerStudy == null)
            {
                return null;
            }

            return _mapper.Map<RegisterStudyMapping>(registerStudy);
        }

        public override async Task<Result> Update(int id, RegisterStudyUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var registerStudy = await _context.RegisterStudys
                    .Include(c => c.LearningProgresses)
                    .FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (registerStudy == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (registerStudy.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                foreach (var item in entity.LearningProgresses)
                {
                    item.UserId = registerStudy.UserId;
                }

                foreach (var lp in registerStudy.LearningProgresses)
                {
                    if (!entity.LearningProgresses.Any(e => e.Id == lp.Id))
                    {
                        _context.LearningProgress.Remove(lp);
                    }
                }

                _context.RegisterStudys.Update(_mapper.Map(entity, registerStudy));

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
