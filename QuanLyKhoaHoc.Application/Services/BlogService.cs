﻿namespace QuanLyKhoaHoc.Application.Services
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
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var blog = _mapper.Map<Blog>(entity);

                blog.CreatorId = int.Parse(_user.Id);

                await _context.Blogs.AddAsync(blog, cancellation);

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
                var blog = await _context.Blogs.FindAsync(id, cancellation);

                if (blog == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (blog.CreatorId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.Blogs.Remove(blog);

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
            var blog = await _context.Blogs.Include(c => c.Creator).FirstOrDefaultAsync(c => c.Id == id, cancellation);

            if (blog == null)
            {
                return null;
            }

            var mapping = _mapper.Map<BlogMapping>(blog);

            mapping.IsLiked = await _context.LikeBlogs.AnyAsync(c => c.BlogId == id);

            return mapping;
        }

        public override async Task<Result> Update(int id, BlogUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var blog = await _context.Blogs.FindAsync(new object[] { id }, cancellation);

                if (blog == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (blog.CreatorId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                var mapping = _mapper.Map(entity, blog);

                mapping.UpdateTime = DateTime.UtcNow;

                _context.Blogs.Update(mapping);

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
