namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IStatisticalService
    {
        Task<decimal> TotalRevenueAsync(DateTime? startDate, DateTime? endDate);

        Task<ICollection<CourseRevenueDto>> TopSellingCoursesAsync(DateTime? startDate, DateTime? endDate, int topN);
    }
}
