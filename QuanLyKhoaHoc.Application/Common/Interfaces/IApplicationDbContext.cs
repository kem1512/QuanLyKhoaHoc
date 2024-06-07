using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Domain.Entities;

namespace QuanLyKhoaHoc.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Answers> Answers { get; }

        DbSet<Bill> Bills { get; }

        DbSet<BillStatus> BillStatuses { get; }

        DbSet<Blog> Blogs { get; }

        DbSet<Certificate> Certificates { get; }

        DbSet<CertificateType> CertificateTypes { get; }

        DbSet<CommentBlog> CommentBlogs { get; }

        DbSet<ConfirmEmail> ConfirmEmails { get; }

        DbSet<Course> Courses { get; }

        DbSet<CourseSubject> CourseSubjects { get; }

        DbSet<District> Districts { get; }

        DbSet<DoHomework> DoHomeworks { get; }

        DbSet<LearningProgress> LearningProgress { get; }

        DbSet<LikeBlog> LikeBlogs { get; }

        DbSet<MakeQuestion> MakeQuestions { get; }

        DbSet<Notification> Notifications { get; }

        DbSet<Permission> Permissions { get; }

        DbSet<Practice> Practices { get; }

        DbSet<ProgramingLanguage> ProgramingLanguages { get; }

        DbSet<Province> Provinces { get; }

        DbSet<RefreshToken> RefreshTokens { get; }

        DbSet<RegisterStudy> RegisterStudys { get; }

        DbSet<Role> Roles { get; }

        DbSet<RunTestCase> RunTestCases { get; }

        DbSet<Subject> Subjects { get; }

        DbSet<SubjectDetail> SubjectDetails { get; }

        DbSet<TestCase> TestCases { get; }

        DbSet<User> Users { get; }

        DbSet<Ward> Wards { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
