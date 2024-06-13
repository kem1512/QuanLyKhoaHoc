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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var commentCommentBlog = _mapper.Map<CommentBlog>(entity);

                commentCommentBlog.UserId = int.Parse(_user.Id);

                await _context.CommentBlogs.AddAsync(commentCommentBlog, cancellation);

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

                var commentCommentBlog = await _context.CommentBlogs.FindAsync(new object[] { id }, cancellation);

                if (commentCommentBlog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (commentCommentBlog.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.CommentBlogs.Remove(commentCommentBlog);

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

        public override async Task<PagingModel<CommentBlogMapping>> Get(CommentBlogQuery query, CancellationToken cancellation)
        {
            var commentCommentBlogs = _context.CommentBlogs.AsNoTracking();

            if(query.BlogId != null)
            {
                commentCommentBlogs = commentCommentBlogs.Where(c => c.BlogId == query.BlogId);
            }

            var totalCount = await commentCommentBlogs.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await commentCommentBlogs
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
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var commentCommentBlog = await _context.CommentBlogs.FindAsync(new object[] { id }, cancellation);

                if (commentCommentBlog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (commentCommentBlog.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.CommentBlogs.Update(_mapper.Map(entity, commentCommentBlog));

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
