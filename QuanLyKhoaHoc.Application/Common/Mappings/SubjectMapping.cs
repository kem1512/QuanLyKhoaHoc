using QuanLyKhoaHoc.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoaHoc.Application.Common.Mappings
{
    public class SubjectMapping
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
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
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Symbol { get; set; } = default!;

        public bool IsActive { get; set; }
    }
}
