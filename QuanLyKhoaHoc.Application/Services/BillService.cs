namespace QuanLyKhoaHoc.Application.Services
{
    public class BillService : ApplicationServiceBase<BillMapping, BillQuery, BillCreate, BillUpdate>
    {
        public BillService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {

        }

        public override async Task<Result> Create(BillCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id.ToString() == _user.Id);

                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == entity.CourseId);

                if (user == null || course == null) return Result.Failure();

                BillMapping bill = new BillMapping()
                {
                    CourseId = entity.CourseId,
                    UserId = int.Parse(_user.Id),
                    BillStatusId = 2,
                    Price = course.Price,
                    TradingCode = Helper.GenerateTradingCode(5)
                };

                await _context.Bills.AddAsync(_mapper.Map<Bill>(bill), cancellation);

                course.NumberOfStudent += 1;
                course.NumberOfPurchases += 1;

                var result = await _context.SaveChangesAsync(cancellation);

                if (entity.VNPaySettings != null)
                {
                    VnPayLibrary vnpay = new VnPayLibrary();

                    vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                    vnpay.AddRequestData("vnp_Command", "pay");
                    vnpay.AddRequestData("vnp_TmnCode", entity.VNPaySettings.vnp_TmnCode);
                    vnpay.AddRequestData("vnp_Amount", ((int)course.Price * 100).ToString());
                    vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    vnpay.AddRequestData("vnp_CurrCode", "VND");
                    vnpay.AddRequestData("vnp_IpAddr", entity.VNPaySettings.ip!);
                    vnpay.AddRequestData("vnp_Locale", "vn");
                    vnpay.AddRequestData("vnp_OrderInfo", "Thanh Toán Đơn Hàng " + bill.Id);
                    vnpay.AddRequestData("vnp_OrderType", "billpayment");
                    vnpay.AddRequestData("vnp_ReturnUrl", entity.VNPaySettings.vnp_ReturnUrl + "/" + course.Id);
                    vnpay.AddRequestData("vnp_TxnRef", bill.TradingCode);

                    string paymentUrl = vnpay.CreateRequestUrl(entity.VNPaySettings.vnp_Url, entity.VNPaySettings.vnp_HashSecret);

                    if (result > 0)
                    {
                        return Result.Success(paymentUrl);
                    }
                }
                else
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
                var bill = await _context.Bills.FindAsync(id, cancellation);

                if (bill == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                _context.Bills.Remove(bill);

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

        public override async Task<PagingModel<BillMapping>> Get(BillQuery query, CancellationToken cancellation)
        {
            var bills = _context.Bills.AsNoTracking();

            var totalCount = await bills.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await bills
                .ApplyQuery(query)
                .ProjectTo<BillMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<BillMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<BillMapping?> Get(int id, CancellationToken cancellation)
        {
            var bill = await _context.Bills.Include(c => c.BillStatus).Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id || c.CourseId == id, cancellation);

            if (bill == null)
            {
                return null;
            }

            return _mapper.Map<BillMapping>(bill);
        }

        public override async Task<Result> Update(int id, BillUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                if (_user.Id == null || !_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Sửa");
                }

                var bill = await _context.Bills.Include(c => c.BillStatus).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (bill == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                if(entity.BillStatusId == 2)
                {
                    var courseSubject = _context.CourseSubjects.Where(c => c.CourseId == bill.CourseId).AsNoTracking();

                    foreach (var x in courseSubject)
                    {
                        await _context.RegisterStudys.AddAsync(new RegisterStudy() { CourseId = x.CourseId, CurrentSubjectId = x.SubjectId, UserId = int.Parse(_user.Id),  });
                    }
                }

                _context.Bills.Update(_mapper.Map(entity, bill));

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
