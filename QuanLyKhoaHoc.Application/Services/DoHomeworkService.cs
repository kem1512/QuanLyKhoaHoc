using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace QuanLyKhoaHoc.Application.Services
{
    public class DoHomeworkService : ApplicationServiceBase<DoHomeworkMapping, DoHomeworkQuery, DoHomeworkCreate, DoHomeworkUpdate>
    {
        public DoHomeworkService(IApplicationDbContext context, IMapper mapper, IUser user) : base(context, mapper, user)
        {
        }

        public override async Task<Result> Create(DoHomeworkCreate entity, CancellationToken cancellation)
        {
            try
            {
                if (_user.Id == null) return new Result(ResultStatus.Forbidden, "Bạn Chưa Đăng Nhập");

                var doHomework = _mapper.Map<DoHomework>(entity);

                doHomework.UserId = int.Parse(_user.Id);

                var testCases = _context.TestCases.Where(c => c.PracticeId == entity.PracticeId).ToList();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .Select(a => a.Location)
                    .ToArray();

                var scriptOptions = ScriptOptions.Default
                    .WithReferences(assemblies)
                    .WithImports("System");

                var output = "";

                foreach (var x in testCases)
                {
                    var scriptCode = $"{entity.ActualOutput} {x.Input}";

                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    var runTestCase = await CSharpScript.EvaluateAsync(scriptCode, scriptOptions);
                    stopwatch.Stop();

                    var runTestCaseString = runTestCase?.ToString() ?? "";
                    output += "," + runTestCaseString;

                    if (doHomework.RunTestCases == null)
                        doHomework.RunTestCases = new List<RunTestCase>();

                    doHomework.RunTestCases.Add(new RunTestCase
                    {
                        Result = runTestCaseString,
                        RunTime = stopwatch.ElapsedMilliseconds,
                        TestCaseId = x.Id
                    });
                }

                doHomework.ActualOutput = output.Substring(1);

                if (doHomework.ActualOutput == string.Join(",", doHomework.RunTestCases.Select(c => c.Result)))
                {
                    doHomework.DoneTime = DateTime.UtcNow;
                    doHomework.HomeworkStatus = HomeworkStatus.Completed;
                    doHomework.IsFinished = true;
                }
                else
                {
                    doHomework.HomeworkStatus = HomeworkStatus.InProgress;
                }

                await _context.DoHomeworks.AddAsync(doHomework, cancellation);
                var result = await _context.SaveChangesAsync(cancellation);

                return result > 0 ? Result.Success() : Result.Failure();
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
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Xóa");
                }

                var doHomework = await _context.DoHomeworks.FindAsync(new object[] { id }, cancellation);

                if (doHomework == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.DoHomeworks.Remove(doHomework);

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

        public override async Task<PagingModel<DoHomeworkMapping>> Get(DoHomeworkQuery query, CancellationToken cancellation)
        {
            var doHomeworks = _context.DoHomeworks.AsNoTracking();

            var totalCount = await doHomeworks.ApplyQuery(query, applyPagination: false).CountAsync();

            var data = await doHomeworks
                .ApplyQuery(query)
                .ProjectTo<DoHomeworkMapping>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellation);

            return new PagingModel<DoHomeworkMapping>(data, totalCount, query.Page ?? 1, query.PageSize ?? 10);
        }

        public override async Task<DoHomeworkMapping?> Get(int id, CancellationToken cancellation)
        {
            var doHomework = await _context.DoHomeworks.Include(c => c.RunTestCases).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id || (c.PracticeId == id && c.UserId.ToString() == _user.Id), cancellation);

            if (doHomework == null)
            {
                return null;
            }

            var mapping = _mapper.Map<DoHomeworkMapping>(doHomework);

            mapping.TestCases = _mapper.Map<ICollection<RunTestCaseMapping>>(doHomework.RunTestCases);

            return mapping;
        }

        public override async Task<Result> Update(int id, DoHomeworkUpdate entity, CancellationToken cancellation)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    return new Result(ResultStatus.Forbidden, "Bạn Không Thể Cập Nhật");
                }

                if (entity.Id != id)
                {
                    return Result.Failure("ID Phải Giống Nhau");
                }

                var doHomework = await _context.DoHomeworks
                    .Include(c => c.RunTestCases)
                    .FirstOrDefaultAsync(c => c.Id == id, cancellation);

                if (doHomework == null)
                {
                    return new Result(ResultStatus.NotFound, "Không Tìm Thấy");
                }

                _context.RunTestCases.RemoveRange(doHomework.RunTestCases);

                var testCases = _context.TestCases.Where(c => c.PracticeId == entity.PracticeId).ToList();

                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .Select(a => a.Location)
                    .ToArray();

                var scriptOptions = ScriptOptions.Default
                    .WithReferences(assemblies)
                    .WithImports("System");
                var output = "";

                foreach (var x in testCases.Select((v, i) => (v, i)))
                {
                    var scriptCode = $"{entity.ActualOutput} {x.v.Input}";

                    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    var runTestCase = await CSharpScript.EvaluateAsync(scriptCode, scriptOptions);
                    stopwatch.Stop();

                    var runTestCaseString = runTestCase?.ToString() ?? "";

                    output += "," + runTestCaseString;

                    if (doHomework.RunTestCases == null)
                        doHomework.RunTestCases = new List<RunTestCase>();

                    doHomework.RunTestCases.Add(new RunTestCase
                    {
                        Result = runTestCaseString,
                        RunTime = stopwatch.ElapsedMilliseconds,
                        TestCaseId = x.v.Id
                    });
                }

                doHomework.ActualOutput = output.Substring(1);

                if (doHomework.ActualOutput == string.Join(",", doHomework.RunTestCases.Select(c => c.Result)))
                {
                    doHomework.DoneTime = DateTime.UtcNow;
                    doHomework.HomeworkStatus = HomeworkStatus.Completed;
                    doHomework.IsFinished = true;
                }
                else
                {
                    doHomework.HomeworkStatus = HomeworkStatus.InProgress;
                    doHomework.IsFinished = false;
                }

                _context.DoHomeworks.Update(_mapper.Map(entity, doHomework));

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
