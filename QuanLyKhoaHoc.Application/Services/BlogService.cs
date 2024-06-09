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
    public class BlogService : ApplicationServiceBase<BlogMapping, BlogQuery, BlogCreate, BlogUpdate>
    {
        public BlogService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(BlogCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var blog = _mapper.Map<Blog>(entity);

                blog.CreatorId = int.Parse(_user.Id);

                await _context.Blogs.AddAsync(blog, cancellation);

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

                var blog = await _context.Blogs.FindAsync(new object[] { id }, cancellation);

                if (blog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (blog.CreatorId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa Bài Viết Này");
                }

                _context.Blogs.Remove(blog);

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

        public override async Task<PagingModel<BlogMapping>> Get(BlogQuery query, CancellationToken cancellation)
        {
            var blogs = _context.Blogs.AsNoTracking();

            var totalCount = await blogs.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await blogs
                .ApplyQuery(query)
                .ProjectTo<BlogMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<BlogMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<BlogMapping?> Get(int id, CancellationToken cancellation)
        {
            var blog = await _context.Blogs.FindAsync(new object[] { id }, cancellation);

            if (blog == null)
            {
                return null;
            }

            return _mapper.Map<BlogMapping>(blog);
        }

        public override async Task<Result> Update(int id, BlogUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var blog = await _context.Blogs.FindAsync(new object[] { id }, cancellation);

                if (blog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (blog.CreatorId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa Khóa Học Này");
                }

                _context.Blogs.Update(_mapper.Map(entity, blog));

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
