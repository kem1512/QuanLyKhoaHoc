using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Common.Models
{
    public class QueryModel
    {
        public string? Filters { get; set; }

        public string? Sorts { get; set; }

        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }
    }
}
