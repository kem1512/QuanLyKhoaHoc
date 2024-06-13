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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var RegisterStudy = _mapper.Map<RegisterStudy>(entity);

                RegisterStudy.UserId = int.Parse(_user.Id);

                await _context.RegisterStudys.AddAsync(RegisterStudy, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
                }

                return Result.Success();
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

                var RegisterStudy = await _context.RegisterStudys.FindAsync(new object[] { id }, cancellation);

                if (RegisterStudy == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (RegisterStudy.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.RegisterStudys.Remove(RegisterStudy);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public override async Task<PagingModel<RegisterStudyMapping>> Get(RegisterStudyQuery query, CancellationToken cancellation)
        {
            var RegisterStudys = _context.RegisterStudys.AsNoTracking();

            var totalCount = await RegisterStudys.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await RegisterStudys
                .ApplyQuery(query)
                .ProjectTo<RegisterStudyMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<RegisterStudyMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<RegisterStudyMapping?> Get(int id, CancellationToken cancellation)
        {
            var RegisterStudy = await _context.RegisterStudys.FindAsync(new object[] { id }, cancellation);

            if (RegisterStudy == null)
            {
                return null;
            }

            return _mapper.Map<RegisterStudyMapping>(RegisterStudy);
        }

        public override async Task<Result> Update(int id, RegisterStudyUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var RegisterStudy = await _context.RegisterStudys.FindAsync(new object[] { id }, cancellation);

                if (RegisterStudy == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (RegisterStudy.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.RegisterStudys.Update(_mapper.Map(entity, RegisterStudy));

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}
