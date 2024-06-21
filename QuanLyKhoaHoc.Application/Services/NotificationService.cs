namespace QuanLyKhoaHoc.Application.Services
{
    public class NotificationService : ApplicationServiceBase<NotificationMapping, NotificationQuery, NotificationCreate, NotificationUpdate>
    {
        public NotificationService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(NotificationCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var Notification = _mapper.Map<Notification>(entity);

                await _context.Notifications.AddAsync(Notification, cancellation);

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
                var Notification = await _context.Notifications.FindAsync(id, cancellation);

                if (Notification == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.Notifications.Remove(Notification);

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

        public override async Task<PagingModel<NotificationMapping>> Get(NotificationQuery query, CancellationToken cancellation)
        {
            var Notifications = _context.Notifications.AsNoTracking();

            var totalCount = await Notifications.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await Notifications
                .ApplyQuery(query)
                .ProjectTo<NotificationMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<NotificationMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<NotificationMapping?> Get(int id, CancellationToken cancellation)
        {
            var Notification = await _context.Notifications.FirstOrDefaultAsync(c => c.Id == id, cancellation);

            if (Notification == null)
            {
                return null;
            }

            var mapping = _mapper.Map<NotificationMapping>(Notification);

            return mapping;
        }

        public override async Task<Result> Update(int id, NotificationUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var Notification = await _context.Notifications.FindAsync(new object[] { id }, cancellation);

                if (Notification == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                var mapping = _mapper.Map(entity, Notification);

                mapping.IsSeen = true;

                _context.Notifications.Update(mapping);

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
