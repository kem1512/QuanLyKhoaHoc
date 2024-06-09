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
    public class CertificateService : ApplicationServiceBase<CertificateMapping, CertificateQuery, CertificateCreate, CertificateUpdate>
    {
        public CertificateService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CertificateCreate entity, CancellationToken cancellation)
        {
            try
            {
                var certificate = _mapper.Map<Certificate>(entity);
                await _context.Certificates.AddAsync(certificate, cancellation);

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
                var certificate = await _context.Certificates.FindAsync(new object[] { id }, cancellation);

                if (certificate == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Certificates.Remove(certificate);

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

        public override async Task<PagingModel<CertificateMapping>> Get(CertificateQuery query, CancellationToken cancellation)
        {
            var certificates = _context.Certificates.AsNoTracking();

            var totalCount = await certificates.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await certificates
                .ApplyQuery(query)
                .ProjectTo<CertificateMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CertificateMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CertificateMapping?> Get(int id, CancellationToken cancellation)
        {
            var certificate = await _context.Certificates.FindAsync(new object[] { id }, cancellation);

            if (certificate == null)
            {
                return null;
            }

            return _mapper.Map<CertificateMapping>(certificate);
        }

        public override async Task<Result> Update(int id, CertificateUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var certificate = await _context.Certificates.FindAsync(new object[] { id }, cancellation);

                if (certificate == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, certificate);

                _context.Certificates.Update(certificate);

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
