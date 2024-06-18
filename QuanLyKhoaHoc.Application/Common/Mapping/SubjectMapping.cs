using QuanLyKhoaHoc.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Common.Mapping
{
    public class SubjectMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }

        public ICollection<SubjectDetailMapping> SubjectDetails { get; set; } = default!;
    }

    public class SubjectQuery : QueryModel { }

    public class SubjectCreate
    {
        [Required(ErrorMessage = "Tên Chủ Đề Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Biểu Tượng Không Được Bỏ Trống")]
        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
    }

    public class SubjectUpdate
    {
        [Required(ErrorMessage = "Chủ Đề Không Được Bỏ Trống")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên Chủ Đề Không Được Bỏ Trống")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Biểu Tượng Không Được Bỏ Trống")]
        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
