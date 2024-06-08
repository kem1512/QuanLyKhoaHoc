﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common.Extension;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Application.Common.Mappings;
using QuanLyKhoaHoc.Application.Common.Models;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Services
{
    public class DistrictService : ApplicationServiceBase<DistrictMapping, DistrictQuery, DistrictCreate, DistrictUpdate>
    {
        public DistrictService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(DistrictCreate entity, CancellationToken cancellation)
        {
            try
            {
                var district = _mapper.Map<District>(entity);
                await _context.Districts.AddAsync(district, cancellation);

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
                var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

                if (district == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Districts.Remove(district);

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

        public override async Task<PagingModel<DistrictMapping>> Get(DistrictQuery query, CancellationToken cancellation)
        {
            var districts = _context.Districts.AsNoTracking();

            var totalCount = await districts.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await districts
                .ApplyQuery(query)
                .ProjectTo<DistrictMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<DistrictMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<DistrictMapping?> Get(int id, CancellationToken cancellation)
        {
            var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

            if (district == null)
            {
                return null;
            }

            return _mapper.Map<DistrictMapping>(district);
        }

        public override async Task<Result> Update(int id, DistrictUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var district = await _context.Districts.FindAsync(new object[] { id }, cancellation);

                if (district == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _mapper.Map(entity, district);

                _context.Districts.Update(district);

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
