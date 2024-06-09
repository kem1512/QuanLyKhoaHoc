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
    public class UserService : ApplicationServiceBase<UserMapping, UserQuery, UserCreate, UserUpdate>
    {
        public UserService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(UserCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var user = _mapper.Map<User>(entity);

                await _context.Users.AddAsync(user, cancellation);

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

                var user = await _context.Users.FindAsync(new object[] { id }, cancellation);

                if (user == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (user.Id != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa Bài Viết Này");
                }

                _context.Users.Remove(user);

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

        public override async Task<PagingModel<UserMapping>> Get(UserQuery query, CancellationToken cancellation)
        {
            var users = _context.Users.AsNoTracking();

            var totalCount = await users.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await users
                .ApplyQuery(query)
                .ProjectTo<UserMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<UserMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<UserMapping?> Get(int id, CancellationToken cancellation)
        {
            var user = await _context.Users.Include(c => c.Province).Include(c => c.District).Include(c => c.Ward).FirstOrDefaultAsync(c => c.Id == id, cancellation);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserMapping>(user);
        }

        public override async Task<Result> Update(int id, UserUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var user = await _context.Users.FindAsync(new object[] { id }, cancellation);

                if (user == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (user.Id != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.Users.Update(_mapper.Map(entity, user));

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
