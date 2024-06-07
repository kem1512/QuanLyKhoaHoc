namespace QuanLyKhoaHoc.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public int? DistrictId { get; set; }

        public int? ProvinceId { get; set; }

        public int? CertificateId { get; set; }

        public int? WardId { get; set; }

        public string Username { get; set; } = default!;

        public DateTime CreateTime { get; set; }

        public string Avatar { get; set; } = default!;

        public string Email { get; set; } = default!;

        public DateTime UpdateTime { get; set; }

        public string Password { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public string Address { get; set; } = default!;

        public UserStatus UserStatus { get; set; }

        public District District { get; set; } = default!;

        public Province Province { get; set; } = default!;

        public Certificate Certificate { get; set; } = default!;

        public Ward Ward { get; set; } = default!;

        public ICollection<Answers> Answers { get; set; } = default!;

        public ICollection<Bill> Bills { get; set; } = default!;

        public ICollection<Blog> Blogs { get; set; } = default!;

        public ICollection<CommentBlog> CommentBlogs { get; set; } = default!;

        public ICollection<ConfirmEmail> ConfirmEmails { get; set; } = default!;

        public ICollection<Course> Courses { get; set; } = default!;

        public ICollection<DoHomework> DoHomeworks { get; set; } = default!;

        public ICollection<LearningProgress> LearningProgresses { get; set; } = default!;

        public ICollection<LikeBlog> LikeBlogs { get; set; } = default!;

        public ICollection<MakeQuestion> MakeQuestions { get; set; } = default!;

        public ICollection<Notification> Notifications { get; set; } = default!;

        public ICollection<Permission> Permissions { get; set; } = default!;

        public ICollection<RefreshToken> RefreshTokens { get; set; } = default!;

        public ICollection<RegisterStudy> RegisterStudies { get; set; } = default!;
    }
}
