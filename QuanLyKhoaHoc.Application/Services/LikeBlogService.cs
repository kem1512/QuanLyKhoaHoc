﻿namespace QuanLyKhoaHoc.Application.Services
{
    public class LikeBlogService : ApplicationServiceBase<LikeBlogMapping, LikeBlogQuery, LikeBlogCreate, LikeBlogUpdate>
    {
        public LikeBlogService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(LikeBlogCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var likeBlog = _mapper.Map<LikeBlog>(entity);

                likeBlog.UserId = int.Parse(_user.Id);

                await _context.LikeBlogs.AddAsync(likeBlog, cancellation);

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

                var likeBlog = await _context.LikeBlogs.FindAsync(new object[] { id }, cancellation);

                if (likeBlog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (likeBlog.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.LikeBlogs.Remove(likeBlog);

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

        public override async Task<PagingModel<LikeBlogMapping>> Get(LikeBlogQuery query, CancellationToken cancellation)
        {
            var likeBlogs = _context.LikeBlogs.AsNoTracking();

            var totalCount = await likeBlogs.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await likeBlogs
                .ApplyQuery(query)
                .ProjectTo<LikeBlogMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<LikeBlogMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<LikeBlogMapping?> Get(int id, CancellationToken cancellation)
        {
            var likeBlog = await _context.LikeBlogs.FindAsync(new object[] { id }, cancellation);

            if (likeBlog == null)
            {
                return null;
            }

            return _mapper.Map<LikeBlogMapping>(likeBlog);
        }

        public override async Task<Result> Update(int id, LikeBlogUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(Domain.ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var likeBlog = await _context.LikeBlogs.FindAsync(new object[] { id }, cancellation);

                if (likeBlog == null)
                {
                    return new Result(Domain.ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (likeBlog.UserId != int.Parse(_user.Id))
                {
                    return new Result(Domain.ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                _context.LikeBlogs.Update(_mapper.Map(entity, likeBlog));

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