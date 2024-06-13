namespace QuanLyKhoaHoc.Application.Services
{
    public class ProgramingLanguageService : ApplicationServiceBase<ProgramingLanguageMapping, ProgramingLanguageQuery, ProgramingLanguageCreate, ProgramingLanguageUpdate>
    {
        public ProgramingLanguageService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(ProgramingLanguageCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");


                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Thêm");
                }

                var programingLanguage = _mapper.Map<ProgramingLanguage>(entity);

                await _context.ProgramingLanguages.AddAsync(programingLanguage, cancellation);

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


                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var programingLanguage = await _context.ProgramingLanguages.FindAsync(new object[] { id }, cancellation);

                if (programingLanguage == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.ProgramingLanguages.Remove(programingLanguage);

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

        public override async Task<PagingModel<ProgramingLanguageMapping>> Get(ProgramingLanguageQuery query, CancellationToken cancellation)
        {
            var programingLanguages = _context.ProgramingLanguages.AsNoTracking();

            var totalCount = await programingLanguages.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await programingLanguages
                .ApplyQuery(query)
                .ProjectTo<ProgramingLanguageMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<ProgramingLanguageMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<ProgramingLanguageMapping?> Get(int id, CancellationToken cancellation)
        {
            var programingLanguage = await _context.ProgramingLanguages.FindAsync(new object[] { id }, cancellation);

            if (programingLanguage == null)
            {
                return null;
            }

            return _mapper.Map<ProgramingLanguageMapping>(programingLanguage);
        }

        public override async Task<Result> Update(int id, ProgramingLanguageUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");


                if (!_user.IsAdministrator)
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var programingLanguage = await _context.ProgramingLanguages.FindAsync(new object[] { id }, cancellation);

                if (programingLanguage == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.ProgramingLanguages.Update(_mapper.Map(entity, programingLanguage));

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
