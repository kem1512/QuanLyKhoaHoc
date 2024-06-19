namespace QuanLyKhoaHoc.Application.Services
{
    public class CertificateTypeService : ApplicationServiceBase<CertificateTypeMapping, CertificateTypeQuery, CertificateTypeCreate, CertificateTypeUpdate>
    {
        public CertificateTypeService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CertificateTypeCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Tạo");
                }

                var certificateType = _mapper.Map<CertificateType>(entity);

                await _context.CertificateTypes.AddAsync(certificateType, cancellation);

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

                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.CertificateTypes.Remove(certificateType);

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

        public override async Task<PagingModel<CertificateTypeMapping>> Get(CertificateTypeQuery query, CancellationToken cancellation)
        {
            var certificateTypes = _context.CertificateTypes.AsNoTracking();

            var totalCount = await certificateTypes.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await certificateTypes
                .ApplyQuery(query)
                .ProjectTo<CertificateTypeMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CertificateTypeMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CertificateTypeMapping?> Get(int id, CancellationToken cancellation)
        {
            var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

            if (certificateType == null)
            {
                return null;
            }

            return _mapper.Map<CertificateTypeMapping>(certificateType);
        }

        public override async Task<Result> Update(int id, CertificateTypeUpdate entity, CancellationToken cancellation)
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

                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, certificateType);

                _context.CertificateTypes.Update(certificateType);

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
