using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common.Extension;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;

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
                var certificateType = _mapper.Map<CertificateType>(entity);

                await _context.CertificateTypes.AddAsync(certificateType, cancellation);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
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
                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.CertificateTypes.Remove(certificateType);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
                }

                return Result.Success();
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
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var certificateType = await _context.CertificateTypes.FindAsync(new object[] { id }, cancellation);

                if (certificateType == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, certificateType);

                _context.CertificateTypes.Update(certificateType);

                var result = await _context.SaveChangesAsync(cancellation);

                if (result != 1)
                {
                    return Result.Failure("Lỗi Gì Đó");
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
