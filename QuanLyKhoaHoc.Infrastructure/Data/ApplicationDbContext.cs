using Microsoft.EntityFrameworkCore;
using QuanLyKhoaHoc.Application.Common.Interfaces;
using QuanLyKhoaHoc.Domain.Entities;
using System.Reflection;

namespace QuanLyKhoaHoc.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Answers> Answers { get; set; }

        public DbSet<Bill> Bills { get; set; }

        public DbSet<BillStatus> BillStatuses { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Certificate> Certificates { get; set; }

        public DbSet<CertificateType> CertificateTypes { get; set; }

        public DbSet<CommentBlog> CommentBlogs { get; set; }

        public DbSet<ConfirmEmail> ConfirmEmails { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseSubject> CourseSubjects { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<DoHomework> DoHomeworks { get; set; }

        public DbSet<LearningProgress> LearningProgress { get; set; }

        public DbSet<LikeBlog> LikeBlogs { get; set; }

        public DbSet<MakeQuestion> MakeQuestions { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Practice> Practices { get; set; }

        public DbSet<ProgramingLanguage> ProgramingLanguages { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<RegisterStudy> RegisterStudys { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RunTestCase> RunTestCases { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SubjectDetail> SubjectDetails { get; set; }

        public DbSet<TestCase> TestCases { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Ward> Wards { get; set; }
    }
}
