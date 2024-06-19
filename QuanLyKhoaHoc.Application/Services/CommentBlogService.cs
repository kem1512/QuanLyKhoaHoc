namespace QuanLyKhoaHoc.Application.Services
{
    public class CommentBlogService : ApplicationServiceBase<CommentBlogMapping, CommentBlogQuery, CommentBlogCreate, CommentBlogUpdate>
    {
        public CommentBlogService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(CommentBlogCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var commentBlog = _mapper.Map<CommentBlog>(entity);

                commentBlog.UserId = int.Parse(_user.Id);

                await _context.CommentBlogs.AddAsync(commentBlog, cancellation);

                var blog = await _context.Blogs.FirstOrDefaultAsync(c => c.Id == entity.BlogId);

                if (blog != null) {
                    blog.NumberOfComments += 1;
                }

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
                var commentBlog = await _context.CommentBlogs.Include(c => c.Blog).FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (commentBlog == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (commentBlog.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.CommentBlogs.Remove(commentBlog);

                if (commentBlog.Blog.NumberOfComments >= 1)
                {
                    commentBlog.Blog.NumberOfComments -= 1;
                }

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

        public override async Task<PagingModel<CommentBlogMapping>> Get(CommentBlogQuery query, CancellationToken cancellation)
        {
            var commentBlog = _context.CommentBlogs.AsNoTracking();

            if (query.ParentId != null)
            {
                int commentPosition = -1;

                commentPosition = commentBlog.ToList().FindIndex(c => c.Id == query.ParentId);

                if (commentPosition != -1)
                {
                    query.Page = (commentPosition / query.PageSize) + 1;
                }
            }

            if (query.BlogId != null)
            {
                commentBlog = commentBlog.Where(c => c.BlogId == query.BlogId);
            }

            var totalCount = await commentBlog.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await commentBlog
                .ApplyQuery(query)
                .ProjectTo<CommentBlogMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<CommentBlogMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<CommentBlogMapping?> Get(int id, CancellationToken cancellation)
        {
            var commentCommentBlog = await _context.CommentBlogs.FindAsync(new object[] { id }, cancellation);

            if (commentCommentBlog == null)
            {
                return null;
            }

            return _mapper.Map<CommentBlogMapping>(commentCommentBlog);
        }

        public override async Task<Result> Update(int id, CommentBlogUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var commentBlog = await _context.CommentBlogs.FindAsync(new object[] { id }, cancellation);

                if (commentBlog == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (commentBlog.UserId.ToString() != _user.Id && !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.CommentBlogs.Update(_mapper.Map(entity, commentBlog));

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
